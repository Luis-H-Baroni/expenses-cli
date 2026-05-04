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
            Console.WriteLine($"TOTAL EXPENSES: ${ParseAmount(ExpensesTotal)}");
            Console.WriteLine($"TOTAL INCOMES: ${ParseAmount(IncomesTotal)}");
            Console.WriteLine("--------------------");
            Console.WriteLine($"BALANCE: ${ParseAmount(Balance)}");
            Console.WriteLine("+------------------+");
        }

        public float ParseAmount(int Amount)
        {
            return (float)Amount / 100;
        }
    };


}