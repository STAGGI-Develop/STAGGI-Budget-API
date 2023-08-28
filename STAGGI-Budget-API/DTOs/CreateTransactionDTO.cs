using STAGGI_Budget_API.Enums;

namespace STAGGI_Budget_API.DTOs
{
    public class CreateTransactionDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public int Type { get; set; }
        public string CreateDate { get; set; }
        public string Category { get; set; }

        // añadir condicionales?
        public string Saving { get; set; }
    }
}
