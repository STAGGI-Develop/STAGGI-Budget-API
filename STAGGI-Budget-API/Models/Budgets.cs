namespace STAGGI_Budget_API.Models
{
    public class Budgets
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public double limitAmount { get; set; }
        public int period { get; set; }
        public int  userId { get; set; }

        Categories category = new Categories();
        public int categoryId { get; set; }
    }
}
