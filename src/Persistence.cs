using System.Text.Json;
using Expenses.src.entities;

namespace Expenses
{
    public class Persistence(DataFile DataFile)
    {
        public DataFile DataFile { get; set; } = DataFile;

        public DataFile getDataFile()
        {
            return DataFile;
        }
        public bool AddExpensesToFile(Row newRow)
        {
            Row? existingId = DataFile.Expenses.Find(row => row.Id == newRow.Id);
            if (existingId != null)
            {
                throw new Exception("Unique ID violation");
            }

            DataFile.Expenses.Add(newRow);

            Console.WriteLine($"AddToFile - {newRow.Name}, {newRow.Value}");
            string updatedJson = JsonSerializer.Serialize(DataFile);

            Console.WriteLine($"AddToFile - {updatedJson}");

            File.WriteAllText("data.json", updatedJson);
            return true;
        }

        public bool AddIncomesToFile(Row row)
        {
            DataFile.Incomes.Add(row);

            Console.WriteLine($"AddToFile - {row.Name}, {row.Value}");
            string updatedJson = JsonSerializer.Serialize(DataFile);

            Console.WriteLine($"AddToFile - {updatedJson}");

            File.WriteAllText("data.json", updatedJson);
            return true;
        }

        public int generateId()
        {
            return Random.Shared.Next(1, 99999);
        }
    }
}