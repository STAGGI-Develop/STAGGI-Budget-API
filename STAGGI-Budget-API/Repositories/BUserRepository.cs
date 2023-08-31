using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Repositories
{
    public class BUserRepository : RepositoryBase<BUser>, IBUserRepository
    {
        public BUserRepository(BudgetContext repositoryContext) : base(repositoryContext)
        {
        }
        public IEnumerable<BUser> GetAll()
        {
            return FindAll()
                //.Include(client => client.Accounts)
                .ToList();
        }
        public BUser? FindById(string id)
        {
            return FindByCondition(budUser => budUser.Id == id)
                //.Include(client => client.Accounts)
                .FirstOrDefault();
        }
        public BUser? FindByEmail(string email)
        {
            return FindByCondition(budUser => budUser.Email.ToUpper() == email.ToUpper())
            .Include(u => u.Account)
            .Include(u => u.Subscription)
            .FirstOrDefault();
        }
        public void Save(BUser budUser)
        {
            if (budUser.Id == string.Empty || budUser.Id == null)
            {
                Create(budUser);
            }
            else
            {
                Update(budUser);
            }
            SaveChanges();
        }
        public void AddUser(BUser budUser)
        {
            Create(budUser);
            SaveChanges();
        }

        public BUser? UserProfile(string email)
        {
            return FindByCondition(budUser => budUser.Email.ToUpper() == email.ToUpper())
                .Include(user => user.Account)
                .ThenInclude(acc => acc.Transactions.OrderByDescending(t => t.Id).Take(6))
                .Include(user => user.Subscription)
                .Include(user => user.Budgets)
                .Include(user => user.Savings)
                .FirstOrDefault();
        }
    }
}
