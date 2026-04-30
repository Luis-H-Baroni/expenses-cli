namespace Expenses.src
{
    public class Remove(Persistence persistence)
    {
        private Persistence persistence = persistence;


        public void Entrypoint(string parameter, string id)
        {
            Console.WriteLine($"Remove - Parameters: {parameter}");
            switch (parameter)
            {
                case "expense":
                    Console.WriteLine($"Remove - Expense - {id}");
                    RemoveExpense(id);
                    break;

                case "income":
                    RemoveIncome(id);
                    Console.WriteLine($"Remove - Income - {id}");
                    break;

                default:
                    Console.WriteLine("Unknown remove parameter");
                    Console.WriteLine("Available remove parameters: expense, income");
                    break;
            }
        }

        private bool RemoveExpense(string id)
        {
            bool success = int.TryParse(id, out int result);
            if (!success)
            {
                Console.WriteLine("Error parsing value");
                return success;
            }

            persistence.RemoveExpensesFromFile(result);
            return true;
        }

        private bool RemoveIncome(string id)
        {
            bool success = int.TryParse(id, out int result);
            if (!success)
            {
                Console.WriteLine("Error parsing value");
                return success;
            }

            persistence.RemoveIncomesFromFile(result);
            return true;
        }
    }
}