using Expenses.src.entities;

namespace Expenses
{

    public class Report(Persistence persistence)
    {
        private readonly Persistence persistence = persistence;

        public void GenerateReport()
        {
            DataFile data = persistence.getDataFile();

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

        public void ListAll()
        {
            Console.WriteLine("ID      TYPE       NAME     VALUE");
            DataFile data = persistence.getDataFile();
            List<ListRow> ExpenseList = data.Expenses.Select(row => new ListRow(row.Id, "Expense", row.Name, row.Value)).ToList();
            List<ListRow> IncomeList = data.Incomes.Select(row => new ListRow(row.Id, "Income", row.Name, row.Value)).ToList();

            foreach (var row in ExpenseList)
            {
                Console.WriteLine($"{row.Id}   {row.Type}    {row.Name}    {row.Value}");
            }
            foreach (var row in IncomeList)
            {
                Console.WriteLine($"{row.Id}   {row.Type}    {row.Name}    {row.Value}");
            }
        }
    }
}