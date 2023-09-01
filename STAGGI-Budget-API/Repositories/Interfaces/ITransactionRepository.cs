using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAll();
        void Save(Transaction transaction);
        Transaction? FindById(int id);
        IEnumerable<Transaction> SearchByKeyword(string searchParameter, string email);
        IEnumerable<Transaction> FindByUserEmail(string email);
        void DeleteTransaction(Transaction transaction);
        IEnumerable<Transaction> SearchByDate(DateTime? fromDate, DateTime? toDate, string email);
        IEnumerable<Transaction> SearchByType(TransactionType type, string email);
        IEnumerable<Transaction> SearchByKeywordAndType(string keyword, TransactionType type, string email);
        IEnumerable<Transaction> SearchByDateAndType(DateTime? fromDate, DateTime? toDate, TransactionType type, string email);
        IEnumerable<Transaction> SearchByKeywordAndDate(string keyword, DateTime? fromDate, DateTime? toDate, string email);
        IEnumerable<Transaction> SearchByAllFilters(string keyword, DateTime? fromDate, DateTime? toDate, TransactionType type, string email);
        IEnumerable<Transaction> SearchLastByEmail(string email);

    }
}
