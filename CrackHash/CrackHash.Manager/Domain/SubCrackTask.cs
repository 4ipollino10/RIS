namespace CrackHash.Manager.Domain;

/// <summary>
/// Сущность подзадачи
/// </summary>
public class SubCrackTask
{
    /// <summary>
    /// Идентификатор подзадачи
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Идентификатор родительской задачи
    /// </summary>
    public Guid ParentTaskId { get; private set; }
    
    /// <summary>
    /// MD5 хэш закодированного слова
    /// </summary>
    public string MD5Hash { get; private set; }
    
    /// <summary>
    /// Максимлаьная длина закодированного слова
    /// </summary>
    public int MaxWordLength { get; private set; }
    
    /// <summary>
    /// Результат взлома хэша
    /// </summary>
    public List<string>? Result { get; set; }
    
    /// <summary>
    /// Статус задачи
    /// </summary>
    public SubCrackTaskStatus Status { get; set; }

    /// <summary>
    /// Количество попыток взома
    /// </summary>
    public int TriesCount { get; private set; }

    /// <summary>
    /// Ctor
    /// </summary>
    public SubCrackTask(Guid parentTaskId, string md5Hash, int maxWordLength)
    {
        Id = Guid.NewGuid();
        ParentTaskId = parentTaskId;
        MD5Hash = md5Hash;
        MaxWordLength = maxWordLength;
        Status = SubCrackTaskStatus.Created;
        TriesCount = 0;
    }
}