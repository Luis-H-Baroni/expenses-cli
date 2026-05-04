using System.CommandLine;
using System.Text.Json;
using Expenses.src.entities;

namespace Expenses.src
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            Persistence persistence = new(LoadDataFile());

            Option<string> nameParameter = new("--name")
            {
                Description = "Transaction display name (\"food\", \"gas\")",
                Required = true
            };
            Option<int> amountParameter = new("--amount")
            {
                Description = "Amount in cents to be registered (1050 -> 10.50)",
                Required = true
            };
            Option<string> transactionTypeParameter = new("--type")
            {
                Description = "Type of transaction (expense, income)",
                Required = true
            };
            Option<int> idParameter = new("--id")
            {
                Description = "Transaction id",
                Required = true
            };

            RootCommand rootCommand = new("Personal expenses cli app");
            Command addCommand = new("add", "Adds a new transaction")
            {
                transactionTypeParameter,
                nameParameter,
                amountParameter
            };
            Command removeCommand = new("remove", "Removes an existing transation")
            {
                transactionTypeParameter,
                idParameter
            };
            Command reportCommand = new("report", "Generates an expenses report");
            Command listCommand = new("list", "List all transactions");

            rootCommand.Subcommands.Add(addCommand);
            rootCommand.Subcommands.Add(removeCommand);
            rootCommand.Subcommands.Add(listCommand);
            rootCommand.Subcommands.Add(reportCommand);

            addCommand.SetAction(parseResult =>
            {
                string type = parseResult.GetValue(transactionTypeParameter)
                    ?? throw new ArgumentException("Missing required option --type");
                string name = parseResult.GetValue(nameParameter)
                    ?? throw new ArgumentException("Missing required option --name");
                int amount = parseResult.GetValue(amountParameter);

                new Add(persistence).Entrypoint(type, name, amount);
            });
            removeCommand.SetAction(parseResult =>
            {
                string type = parseResult.GetValue(transactionTypeParameter)
                    ?? throw new ArgumentException("Missing required option --type");
                int id = parseResult.GetValue(idParameter);

                new Remove(persistence).Entrypoint(type, id);
            });
            listCommand.SetAction(parseResult => new Report(persistence).ListAll());
            reportCommand.SetAction(parseResult => new Report(persistence).GenerateReport());

            return rootCommand.Parse(args).Invoke();

            List<Transaction> LoadDataFile()
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
        }
    }
}