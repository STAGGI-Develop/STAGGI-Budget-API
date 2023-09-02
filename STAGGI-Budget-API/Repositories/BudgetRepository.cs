using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Repositories
{
    public class BudgetRepository: RepositoryBase<Budget>, IBudgetRepository
    {
        public BudgetRepository(BudgetContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Budget> GetAll()
        {
            return FindAll()
                .Include(budget => budget.Category)
                .ToList();
        }
        
        public IEnumerable<Budget> GetAllByEmail(string email)
        {
            return FindAll()
                .Include(budget => budget.Category)
                .Include(budget => budget.BUser)
                .Where(budget => budget.BUser.Email == email)
                .ToList();
        }

        //public Budget? GetById(int id)
        //{
        //    return FindByCondition(budget => budget.Id == id)
        //        .Include(budget => budget.Category)
        //        .FirstOrDefault();
        //}
        public Budget? GetById(int id, bool? includeTransaction = false)
        {
            var query = FindByCondition(budget => budget.Id == id);

            if ((bool)includeTransaction)
            {
                DateTime fromDate = DateTime.UtcNow;

                if (query.First().Period == 0)
                {
                    fromDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                }
                else
                {
                    fromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                }

                query = query.Include(budget => budget.Category)
                    .Include(budget => budget.Transactions.Where(t => t.CreateDate >= fromDate));
            }
            else
            {
                query = query.Include(budget => budget.Category);
            }

            return query.FirstOrDefault();
        }

        public void Save(Budget budget)
        {
            if (budget.Id == 0)
            {
                Create(budget);
            }
            else
            {
                Update(budget);
            }
            SaveChanges();
        }
    }
}
