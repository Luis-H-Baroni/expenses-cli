using System.Text.Json;

namespace Expenses
{
    public class Row(int Id, string name, int value)
    {
        public int Id {get; set;} = Id;
        public string Name { get; set; } = name;
        public int Value { get; set; } = value;
    }

    public class DataFile(List<Row> Expenses, List<Row> Incomes)
    {
        public List<Row> Expenses { get; set; } = Expenses;
        public List<Row> Incomes { get; set; } = Incomes;
    }

    public class Persistence
    {
        public DataFile ReadAndParse()
        {
            bool fileExists = File.Exists("data.json");
            if (fileExists == false)
            {
                string dataJson = JsonSerializer.Serialize(new DataFile([],[]));
                File.WriteAllText("data.json", dataJson);
            }

            string jsonString = File.ReadAllText("data.json");
            DataFile? data = JsonSerializer.Deserialize<DataFile>(jsonString);
            

            foreach (var row in data.Expenses)
            {
                Console.WriteLine($"{row.Name}, {row.Value}");
            }
            foreach (var row in data.Incomes)
            {
                Console.WriteLine($"{row.Name}, {row.Value}");
            }

            return data;
        }

        public bool AddExpensesToFile(Row newRow)
        {
            DataFile data = ReadAndParse();

            Row? existingId = data.Expenses.Find(row => row.Id == newRow.Id);
            if (existingId != null)
            {
                throw new Exception("Unique ID violation");
            }

            data.Expenses.Add(newRow);

            Console.WriteLine($"AddToFile - {newRow.Name}, {newRow.Value}");
            string updatedJson = JsonSerializer.Serialize(data);

            Console.WriteLine($"AddToFile - {updatedJson}");

            File.WriteAllText("data.json", updatedJson);
            return true;
        }

        public bool AddIncomesToFile(Row row)
        {
            DataFile data = ReadAndParse();
            data.Incomes.Add(row);

            Console.WriteLine($"AddToFile - {row.Name}, {row.Value}");
            string updatedJson = JsonSerializer.Serialize(data);

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