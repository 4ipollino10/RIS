using CrackHash.Manager.Application.Services;
using Quartz;

namespace CrackHash.Manager.Application.Jobs;

/// <summary>
/// Джоба для нарезки задач на подзадачи
/// </summary>
/// <param name="taskManagerService"></param>
public class CrackTaskDivideJob(TaskManagerService taskManagerService) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        taskManagerService.DivideCrackTasks();
        return Task.CompletedTask;
    }
}