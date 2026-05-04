using Expenses.src.entities;
using Expenses.src.persistence;

namespace Expenses.src
{
    public class Add(IPersistence persistence)
    {
        private readonly IPersistence persistence = persistence;

        public void Entrypoint(string command, string name, int amount)
        {
            switch (command)
            {
                case "expense":
                    Console.WriteLine($"Add - Expense - {name}: {amount}");
                    AddExpense(name, amount);
                    break;

                case "income":
                    Console.WriteLine($"Add - Income - {name}: {amount}");
                    AddIncome(name, amount);
                    break;
            }

            new Report(persistence).ListAll();
        }

        private bool AddExpense(string name, int amount)
        {
            Transaction Transaction = new(0, "expense", name, amount, DateTime.Now);

            return persistence.Add(Transaction);
        }

        private bool AddIncome(string name, int amount)
        {
            Transaction Transaction = new(0, "income", name, amount, DateTime.Now);

            return persistence.Add(Transaction);
        }
    }
}