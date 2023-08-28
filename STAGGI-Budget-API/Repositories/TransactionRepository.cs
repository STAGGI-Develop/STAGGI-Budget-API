using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BudgetContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Transaction> GetAll()
        {
            return FindAll()
             .ToList();
        }

        public Transaction? FindById(long id)
        {
            return FindByCondition(tr => tr.Id == id)
                .Include(tr => tr.Category)
                .FirstOrDefault();
        }

        public void Save(Transaction transaction)
        {
            Create(transaction);
            SaveChanges();
        }

        public IEnumerable<Transaction> Search(string searchParameter, string userEmail)
        {
            var upperSearch = searchParameter.ToUpper();
            return FindByUserEmail(userEmail).Where(tr =>tr.Title.ToUpper().Contains(upperSearch))
            .ToList();
        }

        public IEnumerable<Transaction> FindByUserEmail(string email)
        {
            return FindAll()
                .Include(tr => tr.Category)
                .Include(tr => tr.Account)
                .ThenInclude(ac => ac.BUser)
                .ThenInclude(bu => bu.Categories)
                .Where(tr=>tr.Account.BUser.Email == email)
                .ToList();
        }
    }
}