using STAGGI_Budget_API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public double Balance { get; set; } = 0;
        public double LimitAmount { get; set; } = 0;
        public BudgetPeriod Period { get; set; }
        public bool IsDisabled { get; set; }

        //With MS doc for One to One
        public string BUserId { get; set; }
        public BUser BUser { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } // = null!;

        public ICollection<Transaction> Transactions { get; set; }


        // // With DataAnotation

        //[ForeignKey("BUserId")]
        //public virtual BUser BUser { get; set; }

        //[ForeignKey("CategoryId")]
        //public virtual Category Category { get; set; }
        //public ICollection<Transaction> Transactions { get; set; }
    }
}
