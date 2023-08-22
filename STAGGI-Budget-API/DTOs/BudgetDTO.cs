using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.DTOs
{
    public class BudgetDTO
    {
        public string? Name { get; set; }
        public double LimitAmount { get; set; }
        public int Period { get; set; }
        public double Balance { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
