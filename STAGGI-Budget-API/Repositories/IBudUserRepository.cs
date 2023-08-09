using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories
{
    public interface IBudUserRepository
    {
        IEnumerable<BudUser> GetAll();
        void Save(BudUser budUser);
        BudUser FindById(long id);
        BudUser FindByEmail(string email);
    }
}
