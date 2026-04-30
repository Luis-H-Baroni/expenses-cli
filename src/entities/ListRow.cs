namespace Expenses.src.entities
{
    public class ListRow(int Id, string Type, string Name, int Value)
    {
        public int Id { get; set; } = Id;
        public string Type { get; set; } = Type;
        public string Name { get; set; } = Name;
        public int Value { get; set; } = Value;
    }
}