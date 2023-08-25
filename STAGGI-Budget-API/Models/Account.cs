using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Account
    {
        public long Id { get; set; }
        public double Balance { get; set; }

        [ForeignKey("BUserId")]
        public virtual BUser? BUser { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
