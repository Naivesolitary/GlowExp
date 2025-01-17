

using System.Text.Json.Serialization;
using System.Text.Json;

namespace GlowExp.Components.Data
{
    public class TransactionStorage
    {
        private readonly FileManager _fileManager;

        public TransactionStorage()
        {
            _fileManager = new FileManager();
        }

        public void SaveTransactions(List<TransactionBase> transactions)
        {
            string json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() } // To handle enums if any
            });
            File.WriteAllText(_fileManager.FilePath, json);
        }

        public List<TransactionBase> LoadTransactions()
        {
            if (!File.Exists(_fileManager.FilePath)) return new List<TransactionBase>();

            string json = File.ReadAllText(_fileManager.FilePath);
            return JsonSerializer.Deserialize<List<TransactionBase>>(json) ?? new List<TransactionBase>();
        }
    }

}
