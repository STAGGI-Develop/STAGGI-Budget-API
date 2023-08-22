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


        public Budget GetById(long id)

        {
            return FindByCondition(budget => budget.Id == id)
                .Include(budget => budget.Category)
                //.Include(client => client.Cards)
                //.Include(client => client.BudUserLoans)
                //    .ThenInclude(cl => cl.Loan)
                .FirstOrDefault();
        }


        public IEnumerable<Budget> GetAll()
        {
            return FindAll()
            //.Include(client => client.Accounts)
            //.Include(client => client.Cards)
            //.Include(client => client.BudUserLoans)
            //    .ThenInclude(cl => cl.Loan)
            .ToList();
        }


        public void Save(Budget budget)
        {
            Create(budget);
            SaveChanges();
        }
    }
}
