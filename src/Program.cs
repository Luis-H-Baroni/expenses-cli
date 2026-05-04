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

            Option<string> nameOption = new("--name")
            {
                Description = "Transaction display name (\"food\", \"gas\")",
                Required = true
            };
            Option<int> amountOption = new("--amount")
            {
                Description = "Amount in cents to be registered (1050 -> 10.50)",
                Required = true
            };
            Option<string> transactionTypeOption = new("--type")
            {
                Description = "Type of transaction (expense, income)",
                Required = true
            };
            Option<int> idOption = new("--id")
            {
                Description = "Transaction id",
                Required = true
            };
            Option<int> monthOption = new("--month")
            {
                Description = "Month for report generation (1-12)",
            };
            Option<int> yearOption = new("--year")
            {
                Description = "year for report generation",
            };

            RootCommand rootCommand = new("Personal expenses cli app");
            Command addCommand = new("add", "Adds a new transaction")
            {
                transactionTypeOption,
                nameOption,
                amountOption
            };
            Command removeCommand = new("remove", "Removes an existing transation")
            {
                idOption
            };
            Command reportCommand = new("report", "Generates an expenses report")
            {
                monthOption,
                yearOption
            };
            Command listCommand = new("list", "List all transactions")
            {
                monthOption,
                yearOption
            };

            rootCommand.Subcommands.Add(addCommand);
            rootCommand.Subcommands.Add(removeCommand);
            rootCommand.Subcommands.Add(listCommand);
            rootCommand.Subcommands.Add(reportCommand);

            addCommand.SetAction(parseResult =>
            {
                string type = parseResult.GetValue(transactionTypeOption)
                    ?? throw new ArgumentException("Missing required option --type");
                string name = parseResult.GetValue(nameOption)
                    ?? throw new ArgumentException("Missing required option --name");
                int amount = parseResult.GetValue(amountOption);

                new Add(persistence).Entrypoint(type, name, amount);
            });
            removeCommand.SetAction(parseResult =>
            {
                int id = parseResult.GetValue(idOption);

                new Remove(persistence).Entrypoint(id);
            });
            listCommand.SetAction(parseResult => new Report(persistence).ListAll(
                parseResult.GetValue(monthOption),
                parseResult.GetValue(yearOption)
            ));
            reportCommand.SetAction(parseResult => new Report(persistence).GenerateReport(
                parseResult.GetValue(monthOption),
                parseResult.GetValue(yearOption)
            ));

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