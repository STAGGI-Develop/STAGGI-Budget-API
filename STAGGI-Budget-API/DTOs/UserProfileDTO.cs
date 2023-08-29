using STAGGI_Budget_API.Models;
using System.Security.Principal;

namespace STAGGI_Budget_API.DTOs
{
    public class UserProfileDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsPremium { get; set; } = false;
        public double Balance { get; set; }
        public SubscriptionDTO? Subscription { get; set; }
        public ICollection<BudgetDTO>? Budgets { get; set; }
        public ICollection<SavingDTO>? Savings { get; set; }
        public ICollection<TransactionDTO>? Transactions { get; set; }
    }
}
