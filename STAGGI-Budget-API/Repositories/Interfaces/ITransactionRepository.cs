using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAll();
        void Save(Transaction transaction);
        Transaction? FindById(int id);
        IEnumerable<Transaction> SearchByKeyword(string searchParameter, string email);
        IEnumerable<Transaction> FindByUserEmail(string email);
        void Delete(Transaction transaction);
        IEnumerable<Transaction> SearchByDate(DateTime? fromDate, DateTime? toDate);
        IEnumerable<Transaction> SearchByType(bool type, string email);
    }
}
