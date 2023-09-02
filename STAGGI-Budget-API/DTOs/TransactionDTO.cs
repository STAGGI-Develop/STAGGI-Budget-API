using STAGGI_Budget_API.Enums;

namespace STAGGI_Budget_API.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string? Type { get; set; }
        public DateTime? CreateDate { get; set; }
        public CategoryDTO? Category { get; set; }
        public BudgetDTO? Budget { get; set; }
        public SavingDTO? Saving { get; set; }
    }
}
