using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Subscription
    {
        [ForeignKey("BUser")]
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public BUser BUser { get; set; }
        //public long BUserId { get; set; }

    }
}
