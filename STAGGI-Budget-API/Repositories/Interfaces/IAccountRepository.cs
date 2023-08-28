using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        void Save(Account account);
        Account FindById(long id);
        IEnumerable<Account> GetAccountsByBUser(string BUserId);
    }
}
