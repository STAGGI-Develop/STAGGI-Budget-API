using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Saving
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public double TargetAmount { get; set; }
        public DateTime? DueDate { get; set; }

        //With MS doc for One to One
        public string BUserId { get; set; }
        public BUser BUser { get; set; } = null!;

        public ICollection<Transaction> Transactions { get; set; }


        // // With DataAnotation
        //[ForeignKey("BUserId")]
        //public virtual BUser BUser { get; set; }
        //public ICollection<Transaction> Transactions { get; set; }
    }
}
