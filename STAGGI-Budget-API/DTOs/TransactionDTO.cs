namespace STAGGI_Budget_API.DTOs
{
    public class TransactionDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public DateTime? CreateDate { get; set; } //Actual por defecto
        public long CategoryId { get; set; }
    }
}
