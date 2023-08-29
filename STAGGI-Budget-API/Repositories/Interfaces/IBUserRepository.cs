using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface IBUserRepository
    {
        IEnumerable<BUser> GetAll();
        void Save(BUser budUser);
        void AddUser(BUser budUser);
        BUser FindById(string id);
        BUser FindByEmail(string email);
        BUser UserProfile(string email);
    }
}
