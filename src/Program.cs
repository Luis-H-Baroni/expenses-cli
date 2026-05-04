using System.CommandLine;
using Expenses.src.persistence;

namespace Expenses.src
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            IPersistence ResolvePersistence(bool useFile)
            {
                return useFile switch
                {
                    true => new FilePersistence(),
                    false => new SqlitePersistence(),
                };
            }

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
            Option<bool> useFileOption = new("--use-file")
            {
                Description = "Use file persistence instead of sqlite",
            };

            RootCommand rootCommand = new("Personal expenses cli app");
            rootCommand.Options.Add(useFileOption);

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

                IPersistence persistence = ResolvePersistence(parseResult.GetValue(useFileOption));

                new Add(persistence).Entrypoint(type, name, amount);
            });
            removeCommand.SetAction(parseResult =>
            {
                int id = parseResult.GetValue(idOption);

                IPersistence persistence = ResolvePersistence(parseResult.GetValue(useFileOption));
                new Remove(persistence).Entrypoint(id);
            });
            listCommand.SetAction(parseResult =>
            {
                IPersistence persistence = ResolvePersistence(parseResult.GetValue(useFileOption));
                new Report(persistence).ListAll(
                    parseResult.GetValue(monthOption),
                    parseResult.GetValue(yearOption)
                );
            });
            reportCommand.SetAction(parseResult =>
            {
                IPersistence persistence = ResolvePersistence(parseResult.GetValue(useFileOption));
                new Report(persistence).GenerateReport(
                    parseResult.GetValue(monthOption),
                    parseResult.GetValue(yearOption)
                );
            });

            return rootCommand.Parse(args).Invoke();
        }


    }
}