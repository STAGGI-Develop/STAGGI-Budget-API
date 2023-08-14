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
        //public BUser? BudUser { get; set; }
        //public long BudUserId { get; set; }
        public Account? Account { get; set; }
        public long AccountId { get; set; }
        public ICollection<TransactionCategory>? TransactionsPerCategory { get; set; }
    }
}
