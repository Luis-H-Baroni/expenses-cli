namespace Expenses
{
    public class Add
    {
        private readonly Persistence persistence = new();
        public void Entrypoint(string parameter, string name, string value)
        {
            Console.WriteLine($"Add - Parameters: {parameter}");
            switch (parameter)
            {
                case "expense":
                    Console.WriteLine($"Add - Expense - {name}: {value}");
                    AddExpense(name, value);

                    break;

                case "income":
                    Console.WriteLine($"Add - Income - {name}: {value}");
                    AddIncome(name, value);
                    break;
                default:
                    Console.WriteLine("Unknown add parameter");
                    break;
            }
        }

        private bool AddExpense(string name, string value)
        {
            bool success = int.TryParse(value, out int result);
            if (!success)
            {
                Console.WriteLine("Error parsing value");
                return success;
            }
            int id = persistence.generateId();
            Row row = new(id, name, result);

            return persistence.AddExpensesToFile(row);
        }

        private bool AddIncome(string name, string value)
        {
            bool success = int.TryParse(value, out int result);
            if (!success)
            {
                Console.WriteLine("Error parsing value");
                return success;
            }
            int id = persistence.generateId();
            Row row = new(id, name, result);

            return persistence.AddIncomesToFile(row);
        }
    }
}