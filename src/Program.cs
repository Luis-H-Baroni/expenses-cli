using System.Text.Json;
using Expenses.src.entities;

namespace Expenses.src
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string command = args.Length > 0 ? args[0] : string.Empty;
            string parameter = args.Length > 1 ? args[1] : string.Empty;
            string name = args.Length > 2 ? args[2] : string.Empty;
            string value = args.Length > 3 ? args[3] : string.Empty;

            bool fileExists = File.Exists("data.json");
            if (fileExists == false)
            {
                string dataJson = JsonSerializer.Serialize(new DataFile([],[]));
                File.WriteAllText("data.json", dataJson);
            }

            string jsonString = File.ReadAllText("data.json");
            DataFile? data = JsonSerializer.Deserialize<DataFile>(jsonString) ?? throw new Exception("Could not load data file");
            Persistence persistence = new(data);    

            switch (command)
            {
                case "add":
                    Add add = new(persistence);
                    add.Entrypoint(parameter, name, value);
                    break;

                case "report":
                    Report report = new(persistence);
                    report.GenerateReport();
                    break;

                default:
                    Console.WriteLine("Unknown command");
                    Console.WriteLine("Available commands: add, report");
                    break;
            }
        }
    }
}