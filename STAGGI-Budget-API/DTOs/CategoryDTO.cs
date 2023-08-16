using STAGGI_Budget_API.Models;
using System.Text.Json.Serialization;

namespace STAGGI_Budget_API.DTOs
{
    public class CategoryDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<TransactionCategory>? TransactionsPerCategory { get; set; }
    }
}
