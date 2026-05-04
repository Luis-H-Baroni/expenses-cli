using System.Text.Json;
using Expenses.src.entities;

namespace Expenses
{
    public class Persistence(List<Transaction> DataFile)
    {
        public List<Transaction> DataFile { get; set; } = DataFile;

        public List<Transaction> getDataFile()
        {
            return DataFile;
        }
        public bool AddExpensesToFile(Transaction newRow)
        {
            Transaction? existingId = DataFile.Find(row => row.Id == newRow.Id);
            if (existingId != null)
            {
                throw new Exception("Unique ID violation");
            }

            DataFile.Add(newRow);

            string updatedJson = JsonSerializer.Serialize(DataFile);

            File.WriteAllText("data.json", updatedJson);
            return true;
        }

        public bool AddIncomesToFile(Transaction row)
        {
            DataFile.Add(row);

            string updatedJson = JsonSerializer.Serialize(DataFile);

            File.WriteAllText("data.json", updatedJson);
            return true;
        }

        public bool RemoveTransactionFromFile(int id)
        {
            Transaction? itemForDeletion = DataFile.Find(x => x.Id == id) ?? throw new Exception("Item not found");

            int indexToDelete = DataFile.IndexOf(itemForDeletion);
            Console.WriteLine($"RemoveTransactionFromFile - {indexToDelete}");

            DataFile.RemoveAt(indexToDelete);

            string updatedJson = JsonSerializer.Serialize(DataFile);
            File.WriteAllText("data.json", updatedJson);

            return true;
        }

                public int generateId()
        {
            return Random.Shared.Next(1, 99999);
        }
    }
}