namespace CrackHash.Common.Models;

/// <summary>
/// Модель добавления задачи
/// </summary>
public sealed record CrackTaskModel
{
    /// <summary>
    /// MD5 хеш
    /// </summary>
    public required string MD5Hash { get; init; }
    
    /// <summary>
    /// Максимальная длина зашифрованного слова
    /// </summary>
    public required int MaxWordLength { get; init; }
}