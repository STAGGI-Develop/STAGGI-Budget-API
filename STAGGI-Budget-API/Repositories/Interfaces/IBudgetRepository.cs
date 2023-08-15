using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        IEnumerable<BUser> GetAll();
        void Save(BUser budUser);
        BUser FindById(long id);
        BUser FindByEmail(string email);
    }
}
