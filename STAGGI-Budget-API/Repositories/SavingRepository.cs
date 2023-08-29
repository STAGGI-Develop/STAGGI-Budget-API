using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Repositories
{
    public class SavingRepository : RepositoryBase<Saving>, ISavingRepository
    {
        public SavingRepository(BudgetContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Saving> GetAll()
        {
            return FindAll()
             .ToList();
        }

        public Saving? FindById(long id)
        {
            return FindByCondition(tr => tr.Id == id)
                .FirstOrDefault();
        }

        public void Save(Saving saving)
        {
            Create(saving);
            SaveChanges();
        }

        public IEnumerable<Saving> Search(string searchParameter)
        {
            var upperSearch = searchParameter.ToUpper();
            return FindByCondition(sv => sv.Name.ToUpper().Contains(upperSearch))
            .ToList();
        }
    }
}
