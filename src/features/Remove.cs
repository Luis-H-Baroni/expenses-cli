namespace Expenses.src
{
    public class Remove(Persistence persistence)
    {
        private Persistence persistence = persistence;

        public void Entrypoint( int id)
        {
            RemoveTransaction(id);
            new Report(persistence).ListAll();
        }

        private bool RemoveTransaction(int id)
        {
            persistence.RemoveTransactionFromFile(id);
            return true;
        }
    }
}