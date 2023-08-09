using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Data
{
    public class BudgetContext  : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options) { }
        public DbSet<BudUser> BudUsers { get; set; }
    }
}
