using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(BudgetContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Account> GetAll()
        {
            return FindAll()
             .ToList();
        }
        public Account? FindById(long id)
        {
            return FindByCondition(acc => acc.Id == id)
                .FirstOrDefault();
        }

        public void Save(Account account)
        {
            if (account.Id == 0) 
            {
                Create(account);
            }
            else
            {
                Update(account);
            }
            
            SaveChanges();
        }

        public IEnumerable<Account> GetAccountsByBUser(string BUserId)
        {
            return FindByCondition(account => account.BUser.Id == BUserId);
        }
    }
}
