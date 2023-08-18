using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        IEnumerable<Budget> GetAll();
        void Save(Budget budget);
        Budget FindById(long id);
    }
}
