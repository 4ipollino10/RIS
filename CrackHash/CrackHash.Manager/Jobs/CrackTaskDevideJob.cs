using CrackHash.Manager.Application.Services;
using Quartz;


namespace CrackHash.Manager.Jobs
{
    public class CrackTaskDevideJob : IJob
    {
        private readonly TaskManagerService _taskManagerService;
        public CrackTaskDevideJob(TaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _taskManagerService.DevideCrackTasks();
            return Task.CompletedTask;
        }
    }
}
