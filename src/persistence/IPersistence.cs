using Expenses.src.entities;

namespace Expenses.src.persistence
{
    public interface IPersistence
    {
        public List<Transaction> GetAll();
        public bool Add(Transaction transaction);
        public bool Remove(int id);
    }
}