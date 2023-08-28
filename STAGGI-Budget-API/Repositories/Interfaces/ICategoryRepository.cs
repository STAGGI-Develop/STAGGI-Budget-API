using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetAllByUserEmail(string email);
        void Save(Category category);
        Category? FindById(long id);
        void DeleteCategory(Category category);
    }
}
