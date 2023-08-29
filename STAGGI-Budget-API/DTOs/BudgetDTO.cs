namespace STAGGI_Budget_API.DTOs
{
    public class BudgetDTO
    {
        public double Balance { get; set; }
        public double LimitAmount { get; set; }
        public string? Period { get; set; }
        public CategoryDTO Category { get; set; }
        public List<TransactionDTO>? Transactions { get; set; }
    }
}
