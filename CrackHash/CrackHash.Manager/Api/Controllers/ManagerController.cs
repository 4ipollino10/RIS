using CrackHash.Manager.Api.Models;
using CrackHash.Manager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrackHash.Manager.Api.Controllers;

/// <summary>
/// Контроллер для задач
/// </summary>
[ApiController, Route("api/v1/crack-manager")]
public class ManagerController(TaskManagerService taskManagerService)
{
    /// <summary>
    /// Добавляет новую задачу
    /// </summary>
    /// <returns>Идентификатор задачи в системе</returns>
    [HttpPut("crack-tasks/add")]
    public Guid AddNewCrackTask(CrackTaskModel model)
    {
        return taskManagerService.AddNewCrackTask(model);
    }

    /// <summary>
    /// Вернет состояние задачи в системе
    /// </summary>
    /// <param name="crackTaskId"></param>
    /// <returns>Состояние задачи в системе</returns>
    [HttpGet("crack-tasks/{crackTaskId:guid}/status")]
    public CrackTaskState GetCrackTaskState(Guid crackTaskId)
    {
        return taskManagerService.GetCrackTaskState(crackTaskId);
    }
}