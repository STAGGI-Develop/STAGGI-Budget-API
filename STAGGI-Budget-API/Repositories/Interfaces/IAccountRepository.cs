using STAGGI_Budget_API.Models;
using System;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        IEnumerable<Account> GetAccountsByBUser(string BUserId);
        void Save(Account account);
        Account FindById(long id);
    }
}
