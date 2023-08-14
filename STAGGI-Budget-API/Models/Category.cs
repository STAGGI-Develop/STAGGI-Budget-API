namespace STAGGI_Budget_API.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<TransactionCategory>? TransactionsPerCategory { get; set; }
    }
}
