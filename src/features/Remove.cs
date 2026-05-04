using Expenses.src.persistence;

namespace Expenses.src
{
    public class Remove(IPersistence persistence)
    {
        private readonly IPersistence persistence = persistence;

        public void Entrypoint(int id)
        {
            RemoveTransaction(id);
            new Report(persistence).ListAll();
        }

        private bool RemoveTransaction(int id)
        {
            persistence.Remove(id);
            return true;
        }
    }
}