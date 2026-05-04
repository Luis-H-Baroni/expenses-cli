using Expenses.src.entities;

namespace Expenses.src.persistence
{
    public class SqlitePersistence : IPersistence
    {
        private readonly AppDbContext _db;

        public SqlitePersistence()
        {
            _db = new AppDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Transaction> GetAll()
        {
            return _db.Transactions.ToList();
        }

        public bool Add(Transaction transaction)
        {
            _db.Transactions.Add(transaction);
            _db.SaveChanges();

            return true;
        }

        public bool Remove(int id)
        {
            Transaction? itemForDeletion = _db.Transactions.Find(id)
            ?? throw new Exception("Item not found");

            _db.Transactions.Remove(itemForDeletion);
            _db.SaveChanges();

            return true;
        }
    }
}