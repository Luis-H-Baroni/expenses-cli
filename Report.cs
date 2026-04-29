namespace Expenses
{
    public class ReportContent(int ExpensesTotal, int IncomesTotal, int Balance)
    {
        public int ExpensesTotal { get; set; } = ExpensesTotal;
        public int IncomesTotal { get; set; } = IncomesTotal;
        public int Balance { get; set; } = Balance;

        public void PrintReport()
        {
            Console.WriteLine("+----- REPORT -----+");
            Console.WriteLine($"TOTAL EXPENSES: {ExpensesTotal}");
            Console.WriteLine($"TOTAL INCOMES: {IncomesTotal}");
            Console.WriteLine("--------------------");
            Console.WriteLine($"BALANCE: {Balance}");
            Console.WriteLine("+------------------+");
        }

    }
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