namespace Expenses.src.entities
{
    public class DataFile(List<Row> Expenses, List<Row> Incomes)
    {
        public List<Row> Expenses { get; set; } = Expenses;
        public List<Row> Incomes { get; set; } = Incomes;
    }
}