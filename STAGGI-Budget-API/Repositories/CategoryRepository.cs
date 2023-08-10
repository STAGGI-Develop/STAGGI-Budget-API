using STAGGI_Budget_API.Data;
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

        public Category? FindById(long id) 
        {
            return FindByCondition(cat => cat.Id == id)
                .FirstOrDefault();
        }

        public void Save(Category category)
        {
            Create(category);
            SaveChanges();
        }
    }
}