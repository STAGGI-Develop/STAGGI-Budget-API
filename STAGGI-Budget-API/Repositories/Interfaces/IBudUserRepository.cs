using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface IBudUserRepository
    {
        IEnumerable<BUser> GetAll();
        void Save(BUser budUser);
        BUser FindById(string id);
        BUser FindByEmail(string email);
    }
}
