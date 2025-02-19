namespace CrackHash.Manager.Domain;

/// <summary>
/// Статусы задачи
/// </summary>
public enum SubCrackTaskStatus
{
    Created = 0,
    InProgress = 1,
    Ready = 2,
    Error = 3
}