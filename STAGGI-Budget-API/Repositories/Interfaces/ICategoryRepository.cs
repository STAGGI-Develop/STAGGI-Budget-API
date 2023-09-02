using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetAllByUserEmail(string email);
        IEnumerable<Category> GetCategoriesWithTransactions(string email, CategoryPeriod period);
        IEnumerable<Category> GetByUserWithBudgets(string email);
        void Save(Category category);
        Category? FindById(int id);
        void DeleteCategory(Category category);
    }
}
