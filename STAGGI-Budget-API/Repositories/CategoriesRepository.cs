using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Repositories
{
    public class CategoriesRepository: RepositoryBase<BudUser>,ICategoriesRepository
    {
        public CategoriesRepository(BudgetContext repositoryContext):base(repositoryContext) 
        {
        }
        public IEnumerable<BudUser> GetAll()
        {
            return FindAll()
                //.Include(client => client.Accounts)
                //.Include(client => client.Cards)
                //.Include(client => client.BudUserLoans)
                //    .ThenInclude(cl => cl.Loan)
                .ToList();
        }
        public BudUser? FindById(long id)
        {
            return FindByCondition(budUser => budUser.Id == id)
                //.Include(client => client.Accounts)
                //.Include(client => client.Cards)
                //.Include(client => client.BudUserLoans)
                //    .ThenInclude(cl => cl.Loan)
                .FirstOrDefault();
        }
        public BudUser? FindByEmail(string email)
        {
            return FindByCondition(budUser => budUser.Email.ToUpper() == email.ToUpper())
            //.Include(client => client.Accounts)
            //.Include(client => client.Cards)
            //.Include(client => client.BudUserLoans)
            //    .ThenInclude(cl => cl.Loan)
            .FirstOrDefault();
        }
        public void Save(BudUser budUser)
        {
            Create(budUser);
            SaveChanges();
        }
    }
}
