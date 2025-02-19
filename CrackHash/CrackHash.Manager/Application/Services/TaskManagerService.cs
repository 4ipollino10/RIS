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

    public void DevideCrackTasks()
    {
        var createdTaskPairs = _crackTasks.Where(pair => pair.Value.Status == CrackTaskStatus.Created);
        foreach (var createdTaskPair in createdTaskPairs)
        {
            createdTaskPair.Value.Devide();
        }
    }
}