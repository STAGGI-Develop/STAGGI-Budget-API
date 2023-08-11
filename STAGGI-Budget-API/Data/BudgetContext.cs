using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Data
{
    public class BudgetContext  : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options) { }
        public DbSet<BudUser> BudUsers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TransactionCategory> TransactionCategory { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Budgets> Budgets { get; set; }

    }
}