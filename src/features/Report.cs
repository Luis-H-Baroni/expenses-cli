using Expenses.src.entities;
using Expenses.src.persistence;

namespace Expenses
{

    public class Report(IPersistence persistence)
    {
        private readonly IPersistence persistence = persistence;

        public void GenerateReport(int month = 0, int year = 0)
        {
            List<Transaction> data = persistence.GetAll();

            int expensesTotal = 0;
            int incomesTotal = 0;

            foreach (var row in data)
            {
                if (month != 0 && row.CreatedAt.Month != month) continue;
                if (year != 0 && row.CreatedAt.Year != year) continue;

                if (row.Type == "expense") expensesTotal += row.Amount;

                if (row.Type == "income") incomesTotal += row.Amount;
            }

            int balance = incomesTotal - expensesTotal;

            ReportContent newReport = new(expensesTotal, incomesTotal, balance);

            new Report(persistence).ListAll(month, year);
            newReport.PrintReport();
        }

        public void ListAll(int month = 0, int year = 0)
        {
            Console.WriteLine("ID      TYPE        NAME          AMOUNT       CREATED AT");
            List<Transaction> data = persistence.GetAll();
            
            foreach (var row in data)
            {
                if (month != 0 && row.CreatedAt.Month != month) continue;
                if (year != 0 && row.CreatedAt.Year != year) continue;
                Console.WriteLine($"{row.Id,-8}{row.Type,-12}{row.Name,-14}${ParseAmount(row.Amount),-12}{row.CreatedAt:yyyy-MM-dd}");
            }
        }

        public float ParseAmount(int Amount)
        {
            return (float)Amount / 100;
        }
    }
}