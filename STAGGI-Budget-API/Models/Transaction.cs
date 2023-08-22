using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace STAGGI_Budget_API.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Type { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [ForeignKey("SavingId")]
        public Saving? Saving { get; set; }
        // uno u otro

        [ForeignKey("BudgetId")]
        public Budget? Budget { get; set; }
    }
}
