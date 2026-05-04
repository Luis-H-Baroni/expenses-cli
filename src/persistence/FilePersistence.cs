using System.Text.Json;
using Expenses.src.entities;

namespace Expenses.src.persistence
{
    public class FilePersistence : IPersistence
    {
        private List<Transaction> _data;

        public FilePersistence()
        {
            _data = Load();
        }

        public List<Transaction> GetAll()
        {
            return _data;
        }

        public bool Add(Transaction transaction)
        {
            transaction.Id = GenerateId();
            
            _data.Add(transaction);
            Save();

            return true;
        }

        public bool Remove(int id)
        {
            Transaction? itemForDeletion = _data.Find(x => x.Id == id)
            ?? throw new Exception("Item not found");

            int indexToDelete = _data.IndexOf(itemForDeletion);
            Console.WriteLine($"Remove - {indexToDelete}");

            _data.RemoveAt(indexToDelete);
            Save();

            return true;
        }

        private void Save()
        {
            File.WriteAllText("data.json", JsonSerializer.Serialize(_data));
        }

        private List<Transaction> Load()
        {
            bool fileExists = File.Exists("data.json");
            if (fileExists == false)
            {
                string dataJson = JsonSerializer.Serialize(new List<Transaction>());
                File.WriteAllText("data.json", dataJson);
            }

            string jsonString = File.ReadAllText("data.json");
            List<Transaction>? data = JsonSerializer.Deserialize<List<Transaction>>(jsonString)
            ?? throw new Exception("Could not load data file");

            return data;
        }

        private int GenerateId()
        {
            return Random.Shared.Next(1, 99999);
        }
    }
}