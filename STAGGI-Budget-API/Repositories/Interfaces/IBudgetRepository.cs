using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        IEnumerable<Budget> GetAll();
        IEnumerable<Budget> GetAllByEmail(string email);
        public Budget GetById(long id);
        void Save(Budget budget);
        
        void Delete(Budget budget);
    }
}
