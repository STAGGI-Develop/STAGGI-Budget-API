using STAGGI_Budget_API.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace STAGGI_Budget_API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public TransactionType Type { get; set; }


        //With MS doc for One to One
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        public int BudgetId { get; set; }
        public Budget? Budget { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int? SavingId { get; set; }
        public Saving? Saving { get; set; }



        // // With DataAnotation

        //[ForeignKey("CategoryId")]
        //public Category Category { get; set; }

        //[ForeignKey("AccountId")]
        //public Account Account { get; set; }

        //[ForeignKey("SavingId")]
        //public Saving? Saving { get; set; }
        //// uno u otro

        //[ForeignKey("BudgetId")]
        //public Budget? Budget { get; set; }
    }
}
