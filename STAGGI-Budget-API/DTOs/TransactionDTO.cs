using STAGGI_Budget_API.Enums;

namespace STAGGI_Budget_API.DTOs
{
    public class TransactionDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime? CreateDate { get; set; } //Actual por defecto
        public CategoryDTO Category { get; set; }
        public SavingDTO Saving { get; set; }
        public BudgetDTO Budget { get; set; }
    }
}
