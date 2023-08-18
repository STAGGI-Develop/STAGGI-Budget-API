using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Budget
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public double LimitAmount { get; set; }
        public int Period { get; set; }

        [ForeignKey("BUserId")]
        //public int BUserId { get; set; }
        public virtual BUser BUser { get; set; }

        [ForeignKey("CategoryId")]
        //public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
