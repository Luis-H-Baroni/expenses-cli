namespace Expenses.src.entities
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
}