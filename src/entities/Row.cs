namespace Expenses.src.entities
{
    public class Row(int Id, string name, int value)
    {
        public int Id { get; set; } = Id;
        public string Name { get; set; } = name;
        public int Value { get; set; } = value;
    }
}