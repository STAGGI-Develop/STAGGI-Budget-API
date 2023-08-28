using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.DTOs
{
    public class BudgetDTO
    {
        public string? Name { get; set; }
        public double LimitAmount { get; set; }
        public BudgetPeriod Period { get; set; }
        public virtual Category? Category { get; set; }
        public double Balance { get; set; }
    }
}
