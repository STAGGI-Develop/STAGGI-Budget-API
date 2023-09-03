namespace STAGGI_Budget_API.DTOs
{
    public class PaginateTransactionsDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<TransactionDTO>? Data { get; set; }
    }
}
