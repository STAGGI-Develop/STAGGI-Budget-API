using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAll();
        void Save(Transaction transaction);
        Transaction? FindById(long id);
    }
}
