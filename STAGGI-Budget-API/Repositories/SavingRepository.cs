using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Repositories
{
    public class SavingRepository : RepositoryBase<Saving>, ISavingRepository
    {
        public SavingRepository(BudgetContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Saving> GetAllByEmail(string email)
        {
            return FindAll()
                .Include(s => s.BUser)
                .Where(s => s.BUser.Email == email)
                .ToList();
        }

        public Saving? GetById(int id, bool? includeTransaction = false)
        {
            var query = FindByCondition(saving => saving.Id == id);

            if ((bool)includeTransaction)
            {
                query = query.Include(saving => saving.Transactions);
            }

            return query.FirstOrDefault();
        }

        public void Save(Saving saving)
        {
            if (saving.Id == 0)
            {
                Create(saving);
            }
            else
            {
                Update(saving);
            }
            SaveChanges();
        }
    }
}
