using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(BudgetContext repositoryContext): base(repositoryContext) { }

        public IEnumerable<Category> GetAll()
        {
            return FindAll()
             .ToList();
        }

        public IEnumerable<Category> GetAllByUserEmail(string email)
        {
            return FindByCondition(cat => cat.BUser.Email == email).ToList();
        }

        public IEnumerable<Category> GetByUserWithBudgets(string email)
        {
            return FindByCondition(cat => cat.BUser.Email == email).Include(cat => cat.Budgets).ToList();
        }

        public Category? FindById(int id) 
        {
            return FindByCondition(cat => cat.Id == id)
                .FirstOrDefault();
        }

        public void Save(Category category)
        {
            if (category.Id == 0)
            {
                Create(category);
            } 
            else
            {
                Update(category);
            }
            SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }

        public IEnumerable<Category> GetCategoriesWithTransactions(string email, CategoryPeriod period)
        {
            int numberOfDays = period == CategoryPeriod.MONTH ? 30 : 7;
            return FindByCondition(cat => cat.BUser.Email == email)
                .Include(cat => cat.Transactions
                    .Where(tr => DateTime.Today < tr.CreateDate.AddDays(numberOfDays) && tr.Type == TransactionType.OUTCOME && tr.Amount < 0))
                .ToList();
        }
    }
}