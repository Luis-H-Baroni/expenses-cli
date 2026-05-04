using Expenses.src.entities;

namespace Expenses.src
{
    public class Add(Persistence persistence)
    {
        private readonly Persistence persistence = persistence;

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
            int id = persistence.generateId();
            Transaction Transaction = new(id, "expense", name, amount, DateTime.Now);

            return persistence.AddExpensesToFile(Transaction);
        }

        private bool AddIncome(string name, int amount)
        {
            int id = persistence.generateId();
            Transaction Transaction = new(id, "income", name, amount, DateTime.Now);

            return persistence.AddIncomesToFile(Transaction);
        }
    }
}