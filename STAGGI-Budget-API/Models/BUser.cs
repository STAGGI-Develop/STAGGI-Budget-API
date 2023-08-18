using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class BUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsPremium { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Budget> Budgets { get; set; }

        //[ForeignKey("SubscriptionId")]
        public Subscription? Subscription { get; set; }
    }
}
