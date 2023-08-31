namespace STAGGI_Budget_API.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public bool IsDisabled { get; set; }
        public List<TransactionDTO>? Transactions { get; set; }
    }
}
