namespace STAGGI_Budget_API.DTOs
{
    public class AccountDTO
    {
        public double Balance { get; set; }
        public List<TransactionDTO>? Transactions { get; set; }
    }
}
