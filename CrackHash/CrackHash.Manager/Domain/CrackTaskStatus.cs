namespace CrackHash.Manager.Domain;

public enum CrackTaskStatus
{
    Created = 0,
    Planned = 1,
    InProgress = 2,
    Ready = 3,  
    Error = 4
}