using Expenses.src.entities;

namespace Expenses
{

    public class Report(Persistence persistence)
    {
        private readonly Persistence persistence = persistence;

        public void GenerateReport()
        {
            List<Transaction> data = persistence.getDataFile();

            int expensesTotal = 0;
            int incomesTotal = 0;

            foreach (var row in data)
            {
                if (row.Type == "expense")
                {
                    expensesTotal += row.Amount;
                }
                if (row.Type == "income")
                {
                    incomesTotal += row.Amount;
                }
            }

            int balance = incomesTotal - expensesTotal;

            ReportContent newReport = new(expensesTotal, incomesTotal, balance);

            new Report(persistence).ListAll();
            newReport.PrintReport();
        }

        public void ListAll()
        {
            Console.WriteLine("ID      TYPE       NAME     AMOUNT       CREATED AT");
            List<Transaction> data = persistence.getDataFile();
            List<Transaction> ExpenseList = data.Where(t => t.Type == "expense").Select(t => new Transaction(t.Id, t.Type, t.Name, t.Amount, t.CreatedAt)).ToList();
            List<Transaction> IncomeList = data.Where(t => t.Type == "income").Select(t => new Transaction(t.Id, t.Type, t.Name, t.Amount, t.CreatedAt)).ToList();

            foreach (var row in ExpenseList)
            {
                Console.WriteLine($"{row.Id}   {row.Type}    {row.Name}    ${ParseAmount(row.Amount)}       {row.CreatedAt}");
            }
            foreach (var row in IncomeList)
            {
                Console.WriteLine($"{row.Id}   {row.Type}    {row.Name}    ${ParseAmount(row.Amount)}       {row.CreatedAt}");
            }
        }

        public float ParseAmount(int Amount)
        {
            return (float)Amount / 100;
        }
    }
}