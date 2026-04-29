using Expenses.src.entities;

namespace Expenses
{
    
    public class Report
    {
        private readonly Persistence persistence = new();

        public void generateReport()
        {
            DataFile data = persistence.ReadAndParse();
            int expensesTotal = 0;
            int incomesTotal = 0;


            foreach (var row in data.Expenses)
            {
                expensesTotal += row.Value;
            }
            foreach (var row in data.Incomes)
            {
                incomesTotal += row.Value;
            }

            int balance = incomesTotal - expensesTotal;

            ReportContent newReport = new(expensesTotal, incomesTotal, balance);
            newReport.PrintReport();

        }
    }
}