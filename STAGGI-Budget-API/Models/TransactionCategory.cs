namespace STAGGI_Budget_API.Models
{
    public class TransactionCategory
    {
        public long Id { get; set; }        
        public Transaction? Transaction { get; set; }
        public long TransactionId { get; set; }       
        public Category? Category { get; set; }
        public long CategoryId { get; set; }
    }
}
