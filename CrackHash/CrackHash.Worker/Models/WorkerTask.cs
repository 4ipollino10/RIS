namespace CrackHash.Worker.Models
{
    public class WorkerTask
    {
        public Guid TaskId { get; set; }
        public Guid ParentTaskId { get; set; }
        public string Hash { get; set; } = string.Empty;
        public int MaxLength { get; set; }
        public int PartNumber { get; set; }
        public int PartCount { get; set; }
    }

    public class WorkerTaskResult
    {
        public Guid TaskId { get; set; }
        public Guid ParentTaskId { get; set; }
        public List<string> Words { get; set; } = new List<string>();
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
    }
}