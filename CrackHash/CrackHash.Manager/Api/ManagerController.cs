using CrackHash.Common.Models;
using CrackHash.Manager.Application;
using CrackHash.Manager.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CrackHash.Manager.Api;

/// <summary>
/// Контроллер для задач взлома MD5 хеша
/// </summary>
[ApiController, Route("api/v1/crack-manager")]
public class ManagerController(TaskManagerService taskManagerService)
{
    /// <summary>
    /// Добавляет новую задачу на взлом MD5 хеша
    /// </summary>
    /// <returns>Идентификатор задачи в системе</returns>
    [HttpPut("crack-tasks/add")]
    public Guid AddNewCrackTask(CrackTaskModel model)
    {
        return taskManagerService.AddNewCrackTask(model);
    }

    [HttpGet("crack-tasks")]
    public List<CrackTask> GetTasks()
    {
        return taskManagerService.GetTasks();
    }
}