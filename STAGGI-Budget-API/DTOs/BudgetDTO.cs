using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.DTOs
{
    public class BudgetDTO
    {
        public string? Name { get; set; }
        public double LimitAmount { get; set; }
        public int Period { get; set; }
        public int BUserId { get; set; }
        public BUser BUser { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
