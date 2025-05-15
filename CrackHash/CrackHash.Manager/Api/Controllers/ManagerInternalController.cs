using CrackHash.Manager.Application.Services;
using CrackHash.Worker.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("internal/api/manager")]
public class ManagerInternalController : ControllerBase
{
    private readonly TaskManagerService _taskManagerService;
    
    public ManagerInternalController(TaskManagerService taskManagerService)
    {
        _taskManagerService = taskManagerService;
    }
    
    [HttpPost("hash/crack/task/complete")]
    public IActionResult CompleteTask([FromBody] WorkerTaskResult result)
    {
        _taskManagerService.CompleteSubTask(
            result.ParentTaskId,
            result.Words,
            result.Error);
        
        return Ok();
    }
}