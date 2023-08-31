namespace STAGGI_Budget_API.DTOs
{
    public class SavingDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public double TargetAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public List<TransactionDTO>? Transactions { get; set; }
    }
}
