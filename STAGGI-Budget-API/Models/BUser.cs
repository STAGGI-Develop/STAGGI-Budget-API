using System.Security.Principal;

namespace STAGGI_Budget_API.Models
{
    public class BUser
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsPremium { get; set; }
        public ICollection<Account> Accounts { get; set; }

        public Subscription Subscription { get; set; }
        //Relación establecida en Subscrition
        //public long SubscriptionId { get; set; }
    }
}
