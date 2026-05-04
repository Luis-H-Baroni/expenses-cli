namespace Expenses.src
{
    public class Remove(Persistence persistence)
    {
        private Persistence persistence = persistence;

        public void Entrypoint(string parameter, int id)
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

            new Report(persistence).ListAll();
        }

        private bool RemoveExpense(int id)
        {
            persistence.RemoveExpensesFromFile(id);
            return true;
        }

        private bool RemoveIncome(int id)
        {
            persistence.RemoveIncomesFromFile(id);
            return true;
        }
    }
}