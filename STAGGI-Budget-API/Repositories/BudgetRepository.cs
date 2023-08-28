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
                //    .ThenInclude(cl => cl.Loan)
                .ToList();
        }
        
        public IEnumerable<Budget> GetAllByEmail(string email)
        {
            return FindAll()
                .Include(budget => budget.Category)
                .Include(budget => budget.BUser)
                .Where(budget => budget.BUser.Email == email)
                //    .ThenInclude(cl => cl.Loan)
                .ToList();
        }

        public Budget GetById(long id)
        {
            return FindByCondition(budget => budget.Id == id)
                .Include(budget => budget.Category)
                //    .ThenInclude(cl => cl.Loan)
                .FirstOrDefault();
        }

        public void Save(Budget budget)
        {
            Create(budget);
            SaveChanges();
        }
    }
}
