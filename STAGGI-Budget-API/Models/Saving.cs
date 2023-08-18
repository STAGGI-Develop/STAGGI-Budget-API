using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Saving
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public double TargetAmount { get; set; }
        public DateTime? DueDate { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}
