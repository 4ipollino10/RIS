using System.Collections.Concurrent;
using CrackHash.Common.Models;
using CrackHash.Manager.Domain;

namespace CrackHash.Manager.Application;

public class TaskManagerService
{
    private readonly ConcurrentDictionary<Guid, CrackTask> _tasks = new();
    
    /// <summary>
    /// Добавит новую задачу в систему
    /// </summary>
    /// <returns>Идентификатор задачи в системе</returns>
    public Guid AddNewCrackTask(CrackTaskModel model)
    {
        var newTask = new CrackTask(model);
        _tasks.TryAdd(newTask.Id, newTask);
        return newTask.Id;
    }

    public List<CrackTask> GetTasks()
    {
        return _tasks.Select(x => x.Value).ToList();
    }
}