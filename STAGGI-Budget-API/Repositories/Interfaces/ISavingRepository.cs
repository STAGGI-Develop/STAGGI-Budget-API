
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories.Interfaces
{
    public interface ISavingRepository
    {
        IEnumerable<Saving> GetAll();
        void Save(Saving saving);
        Saving? FindById(long id);
        IEnumerable<Saving> Search(string searchParameter);
    }
}
