using CrackHash.Worker.Models;
using CrackHash.Worker.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrackHash.Worker.Controllers
{
    [ApiController]
    [Route("internal/api/worker")]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerTaskService _taskService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<WorkerController> _logger;

        public WorkerController(
            WorkerTaskService taskService,
            IHttpClientFactory httpClientFactory,
            ILogger<WorkerController> logger)
        {
            _taskService = taskService;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpPost("hash/crack/task")]
        public async Task<IActionResult> ProcessTask([FromBody] WorkerTask request)
        {
            try
            {
                _logger.LogInformation($"Received task for request {request.ParentTaskId}");

                var words = _taskService.ProcessTask(request);

                var result = new WorkerTaskResult
                {
                    TaskId = request.TaskId,
                    ParentTaskId = request.ParentTaskId,
                    Words = words,
                    IsSuccess = true
                };

                _logger.LogDebug($"Sending response: {System.Text.Json.JsonSerializer.Serialize(result)}");

                var client = _httpClientFactory.CreateClient("ManagerClient");
                var response = await client.PostAsJsonAsync(
                    "internal/api/manager/hash/crack/task/complete", 
                    result);

                return response.IsSuccessStatusCode ? Ok(result) : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing task");
                
                var errorResult = new WorkerTaskResult
                {
                    TaskId = request?.TaskId ?? Guid.Empty,
                    ParentTaskId = request?.ParentTaskId ?? Guid.Empty,
                    IsSuccess = false,
                    Error = ex.Message
                };

                return BadRequest(errorResult);
            }
        }
    }
}