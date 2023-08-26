using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAll();
        void Save(Transaction transaction);
        Transaction? FindById(long id);
        IEnumerable<Transaction> Search(string searchParameter, string userEmail);
        IEnumerable<Transaction> FindByUserEmail(string email);
    }
}
