using CrackHash.Manager.Api.Models;

namespace CrackHash.Manager.Domain;

public class CrackTask
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// MD5 хэш закодированного слова
    /// </summary>
    public string MD5Hash { get; private set; }
    
    /// <summary>
    /// Максимлаьная длина закодированного слова
    /// </summary>
    public int MaxWordLength { get; private set; }
    
    /// <summary>
    /// Статус задачи
    /// </summary>
    public CrackTaskStatus Status { get; private set; }
    
    /// <summary>
    /// Список нарезанных подзадач для Woker
    /// </summary>
    public List<SubCrackTask> SubTasks { get; private set; } = [];
    
    /// <summary>
    /// Результат взлома хэша
    /// </summary>
    public List<string>? Result { get; private set; }
    
    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string? ErrorMessage { get; private set; }
    
    public CrackTask(CrackTaskModel model)
    {
        Id = Guid.NewGuid();
        MD5Hash = model.MD5Hash;
        MaxWordLength = model.MaxWordLength;
        Status = CrackTaskStatus.Created;
    }

    public void Devide()
    { 
        var subCrackTask = new SubCrackTask(Id, MD5Hash, MaxWordLength);
        SubTasks.Add(subCrackTask);
        Status = CrackTaskStatus.Planned;
    }
}