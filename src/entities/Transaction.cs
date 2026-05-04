namespace Expenses.src.entities
{
    public class Transaction(int Id, string type, string Name, int Amount, DateTime CreatedAt)
    {
        public int Id { get; set; } = Id;
        public string Type { get; set; } = type;
        public string Name { get; set; } = Name;
        public int Amount { get; set; } = Amount;
        public DateTime CreatedAt { get; set; } = CreatedAt;
    }
}