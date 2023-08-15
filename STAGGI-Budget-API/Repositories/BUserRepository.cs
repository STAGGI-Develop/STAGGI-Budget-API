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
        public BUser? FindById(long id)
        {
            return FindByCondition(budUser => budUser.Id == id)
                //.Include(client => client.Accounts)
                .FirstOrDefault();
        }
        public BUser? FindByEmail(string email)
        {
            return FindByCondition(budUser => budUser.Email.ToUpper() == email.ToUpper())
            //.Include(client => client.Accounts)
            .FirstOrDefault();
        }
        public void Save(BUser budUser)
        {
            Create(budUser);
            SaveChanges();
        }
    }
}
