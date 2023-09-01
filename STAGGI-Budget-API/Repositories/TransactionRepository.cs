using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using System.ComponentModel;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
            return FindByUserEmail(email).Where(tr => tr.Title.ToUpper().Contains(upperSearch) || (!string.IsNullOrEmpty(tr.Description) && tr.Description.ToUpper().Contains(upperSearch)))
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

        public IEnumerable<Transaction> SearchByDate(DateTime? fromDate, DateTime? toDate, string email)
        {
            //return FindByCondition(tr => tr.CreateDate >= fromDate && tr.CreateDate <= toDate)
            //.ToList();

            return FindByUserEmail(email).Where(tr => tr.CreateDate >= fromDate && tr.CreateDate <= toDate)
            .ToList();
        }

        public IEnumerable<Transaction> SearchByType(TransactionType type, string email)
        {
            return FindByUserEmail(email).Where(tr => tr.Type == type)
            .ToList();
        }

        public IEnumerable<Transaction> SearchByKeywordAndType(string keyword, TransactionType type, string email)
        {
            var upperSearch = keyword.ToUpper();
            return FindByUserEmail(email).Where(tr => tr.Type == type && tr.Title.ToUpper().Contains(upperSearch))
            .ToList();
        }

        public IEnumerable<Transaction> SearchByDateAndType(DateTime? fromDate, DateTime? toDate, TransactionType type, string email)
        {
            return FindByUserEmail(email).Where(tr => tr.CreateDate >= fromDate && tr.CreateDate <= toDate && tr.Type == type)
            .ToList();
        }

        public IEnumerable<Transaction> SearchByKeywordAndDate(string keyword, DateTime? fromDate, DateTime? toDate, string email)
        {
            var upperSearch = keyword.ToUpper();
            return FindByUserEmail(email).Where(tr => tr.Title.ToUpper().Contains(upperSearch) && tr.CreateDate >= fromDate && tr.CreateDate <= toDate)
            .ToList();
        }

        public IEnumerable<Transaction> SearchByAllFilters(string keyword, DateTime? fromDate, DateTime? toDate, TransactionType type, string email)
        {
            var upperSearch = keyword.ToUpper();
            return FindByUserEmail(email).Where(tr => tr.Title.ToUpper().Contains(upperSearch) && tr.CreateDate >= fromDate && tr.CreateDate <= toDate && tr.Type == type)
            .ToList();
        }

        public IEnumerable<Transaction> SearchLastByEmail(string email)
        {
            return FindByUserEmail(email).OrderByDescending(tr => tr.CreateDate).Take(6).ToList();
        }
    }
}