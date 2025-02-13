using CrackHash.Common.Models;

namespace CrackHash.Manager.Domain;

public class CrackTask
{
    public Guid Id { get; private set; }
    public string MD5Hash { get; private set; }
    
    public int MaxWordLength { get; private set; }
    
    public CrackTaskStatus Status { get; private set; }
    
    public List<SubCrackTask> SubTasks { get; private set; } = [];
    
    public List<string>? Result { get; private set; }
    
    public string? ErrorMessage { get; private set; }
    
    public CrackTask(CrackTaskModel model)
    {
        Id = Guid.NewGuid();
        MD5Hash = model.MD5Hash;
    }
}