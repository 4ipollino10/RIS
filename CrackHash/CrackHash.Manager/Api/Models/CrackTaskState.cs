using CrackHash.Manager.Domain;

namespace CrackHash.Manager.Api.Models;

/// <summary>
/// Класс-представление <see cref="CrackHash.Manager.Domain.CrackTask"/>
/// </summary>
public sealed record CrackTaskState
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// MD5 хэш закодированного слова
    /// </summary>
    public required string MD5Hash { get; init; }
    
    /// <summary>
    /// Максимлаьная длина закодированного слова
    /// </summary>
    public required int MaxWordLength { get; init; }
    
    /// <summary>
    /// Статус задачи
    /// </summary>
    public required CrackTaskStatus Status { get; init; }

    /// <summary>
    /// Процент выполнения задачи
    /// </summary>
    public required string TaskProgressPercent { get; init; }
    
    /// <summary>
    /// Результат взлома хэша
    /// </summary>
    public string? Result { get; init; }
    
    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string? ErrorMessage { get; init; }
}