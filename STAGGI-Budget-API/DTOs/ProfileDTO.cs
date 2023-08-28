using STAGGI_Budget_API.Models;
using System.Security.Principal;

namespace STAGGI_Budget_API.DTOs
{
    public class ProfileDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsPremium { get; set; } = false;
        public string? SubscriptionEnd { get; set; }
        public AccountDTO Account { get; set; }
        public ICollection<BudgetDTO>? Budgets { get; set; }
        public ICollection<SavingDTO>? Savings { get; set; }
        public ICollection<TransactionDTO>? Transactions { get; set; }
    }
}
