using STAGGI_Budget_API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Balance { get; set; }
        public double LimitAmount { get; set; }
        public BudgetPeriod Period { get; set; }

        //With MS doc for One to One
        public string BUserId { get; set; }
        public BUser BUser { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Transaction> Transactions { get; set; }


        // // With DataAnotation

        //[ForeignKey("BUserId")]
        //public virtual BUser BUser { get; set; }

        //[ForeignKey("CategoryId")]
        //public virtual Category Category { get; set; }
        //public ICollection<Transaction> Transactions { get; set; }
    }
}
