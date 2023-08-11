namespace STAGGI_Budget_API.Models
{
    public class Categories
    {
        public long Id { get; set; }
        public string? name { get; set; }
        public string? imageUrl { get; set; } 

        public ICollection<Budgets> budgets { get; set; }
    }
}
