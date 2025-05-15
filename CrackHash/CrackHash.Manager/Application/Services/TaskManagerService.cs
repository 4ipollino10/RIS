using System.Collections.Concurrent;
using CrackHash.Manager.Api.Models;
using CrackHash.Manager.Application.Exceptions;
using CrackHash.Manager.Domain;

namespace CrackHash.Manager.Application.Services;

public class TaskManagerService
{
    private readonly ConcurrentDictionary<Guid, CrackTask> _crackTasks = new();
    
    /// <summary>
    /// Добавит новую задачу в систему
    /// </summary>
    /// <returns>Идентификатор задачи в системе</returns>
    public Guid AddNewCrackTask(CrackTaskModel model)
    {
        var newTask = new CrackTask(model);
        _crackTasks.TryAdd(newTask.Id, newTask);
        return newTask.Id;
    }

    /// <summary>
    /// Вернет состояние задачи в системе
    /// </summary>
    /// <param name="crackTaskId"></param>
    /// <returns>Состояние задачи в системе</returns>
    /// <exception cref="NotFoundException">Выбрасывавется в случае обращения к несуществующей задаче</exception>
    public CrackTaskState GetCrackTaskState(Guid crackTaskId)
    {
        if (!_crackTasks.TryGetValue(crackTaskId, out var crackTask))
        {
            throw new NotFoundException($"Задачи с Id [{crackTaskId}] не существует!");
        }

        var subTasksReady = crackTask.SubTasks.Count(x => x.Status == SubCrackTaskStatus.Ready);
        
        return new CrackTaskState
        {
            Id = crackTask.Id,
            MD5Hash = crackTask.MD5Hash,
            MaxWordLength = crackTask.MaxWordLength,
            Status = crackTask.Status,
            TaskProgressPercent = crackTask.SubTasks.Count != 0 
                ? $"{subTasksReady / crackTask.SubTasks.Count}%" 
                : "0%",
            Result = crackTask.Result != null 
                ? string.Join(", ", crackTask.Result)
                : null,
            ErrorMessage = crackTask.ErrorMessage
        };
    }

    /// <summary>
    /// Разделит задачи в статусе <see cref="CrackHash.Manager.Domain.CrackTaskStatus.Created"/> на подзадачи
    /// </summary>
    public void DivideCrackTasks()
    {
        var createdTaskPairs = _crackTasks.Where(pair => pair.Value.Status == CrackTaskStatus.Created);
        foreach (var createdTaskPair in createdTaskPairs)
        {
            createdTaskPair.Value.Divide();
        }
    }
    
    public void CompleteSubTask(Guid parentTaskId, List<string>? words, string? error)
    {
        if (!_crackTasks.TryGetValue(parentTaskId, out var crackTask))
            return;

        var subTask = crackTask.SubTasks.FirstOrDefault(t => t.ParentTaskId == parentTaskId);
        if (subTask == null)
            return;

        if (error != null)
        {
            subTask.Status = SubCrackTaskStatus.Error;
            crackTask.ErrorMessage = error;
            crackTask.Status = CrackTaskStatus.Error;
        }
        else
        {
            subTask.Result = words;
            subTask.Status = SubCrackTaskStatus.Ready;
        
            if (crackTask.SubTasks.All(t => t.Status == SubCrackTaskStatus.Ready))
            {
                crackTask.Result = crackTask.SubTasks
                    .SelectMany(t => t.Result ?? new List<string>())
                    .ToList();
                crackTask.Status = CrackTaskStatus.Ready;
            }
        }
    }
    
}