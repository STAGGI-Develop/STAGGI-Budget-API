using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        [ForeignKey("BUserId")]
        public virtual BUser BUser { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
