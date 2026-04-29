using Expenses.src;

namespace Expenses
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string command = args.Length > 0 ? args[0] : string.Empty;
            string parameter = args.Length > 1 ? args[1] : string.Empty;
            string name = args.Length > 2 ? args[2] : string.Empty;
            string value = args.Length > 3 ? args[3] : string.Empty;

            switch (command)
            {
                case "add":
                    Add add = new();
                    add.Entrypoint(parameter, name, value);
                    
                    break;

                case "report":
                    Report report = new();
                    report.generateReport();
                    break;
                default:
                    
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}