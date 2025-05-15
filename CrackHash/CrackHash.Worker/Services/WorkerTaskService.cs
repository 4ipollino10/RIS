using System.Security.Cryptography;
using System.Text;
using CrackHash.Worker.Models;

namespace CrackHash.Worker.Services
{
    public class WorkerTaskService
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        
        public List<string> ProcessTask(WorkerTask task)
        {
            var results = new List<string>();
            
            for (int length = 1; length <= task.MaxLength; length++)
            {
                var totalCombinations = (long)Math.Pow(Alphabet.Length, length);
                var partSize = totalCombinations / task.PartCount;
                var start = (task.PartNumber - 1) * partSize;
                var end = task.PartNumber == task.PartCount ? totalCombinations : start + partSize;
                
                GenerateCombinations(Alphabet.ToCharArray(), length, start, end, task.Hash, results);
            }
            
            return results;
        }
        
        private void GenerateCombinations(char[] alphabet, int length, long start, long end, string targetHash, List<string> results)
        {
            // Упрощенная реализация генератора комбинаций
            var indices = new int[length];
            long current = 0;
            
            while (current < end)
            {
                if (current >= start)
                {
                    var word = new string(indices.Select(i => alphabet[i]).ToArray());
                    var hash = CalculateMd5Hash(word);
                    
                    if (hash == targetHash)
                    {
                        results.Add(word);
                    }
                }
                
                // Увеличиваем индексы
                for (int i = length - 1; i >= 0; i--)
                {
                    if (indices[i] < alphabet.Length - 1)
                    {
                        indices[i]++;
                        break;
                    }
                    indices[i] = 0;
                }
                
                current++;
            }
        }
        
        private string CalculateMd5Hash(string input)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}