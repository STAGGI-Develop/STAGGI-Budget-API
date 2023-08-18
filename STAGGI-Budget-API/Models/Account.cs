using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public double Balance { get; set; }
        public bool isPrincipal { get; set; }

        [ForeignKey("BUserId")]
        public virtual BUser? BUser { get; set; }

        //[ForeignKey("SavingId")]
        public virtual Saving? Saving { get; set; }
    }
}
