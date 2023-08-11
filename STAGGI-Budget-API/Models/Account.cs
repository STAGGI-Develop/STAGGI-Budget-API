namespace STAGGI_Budget_API.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public double Balance { get; set; }
        public bool isPrincipal { get; set; }
        public BudUser? BudUser { get; set; }
        public long BudUserId { get; set; }
    }
}
