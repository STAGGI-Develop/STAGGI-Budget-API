using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.DTOs
{
    public class CreateBudgetDTO
    {
        public string? Name { get; set; }
        public double LimitAmount { get; set; }
        public string Period { get; set; }
        public string Category { get; set; }
    }
}
