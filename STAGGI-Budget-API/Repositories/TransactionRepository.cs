using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using System.Linq;

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

        public Transaction? FindById(int id)
        {
            return FindByCondition(tr => tr.Id == id)
                .FirstOrDefault();
        }

        public void Save(Transaction transaction)
        {
            if (transaction.Id == null)
            {
                Create(transaction);
            }
            else
            {
                Update(transaction);
            }
            SaveChanges();
        }

        public IEnumerable<Transaction> SearchByKeyword(string searchParameter, string email)
        {
            var upperSearch = searchParameter.ToUpper();
            return FindByUserEmail(email).Where(tr => tr.Title.ToUpper().Contains(upperSearch))
            .ToList();
        }

        public IEnumerable<Transaction> FindByUserEmail(string email)
        {
            return FindAll()
                .Include(tr => tr.Category)
                .Include(tr => tr.Account)
                .ThenInclude(ac => ac.BUser)
                .ThenInclude(bu => bu.Categories)
                .Where(tr => tr.Account.BUser.Email == email)
                .ToList();
        }

        public void Delete(Transaction transaction)
        {               
            Delete(transaction);
            SaveChanges();            
        }

        public IEnumerable<Transaction> SearchByDate(DateTime? fromDate, DateTime? toDate)
        {
            return FindByCondition(tr => tr.CreateDate >= fromDate && tr.CreateDate <= toDate)
            .ToList();
        }

        public IEnumerable<Transaction> SearchByType(bool type, string email)
        {
            throw new NotImplementedException();
        }
    }
}