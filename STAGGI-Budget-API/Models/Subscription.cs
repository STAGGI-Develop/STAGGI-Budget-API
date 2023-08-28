using System.ComponentModel.DataAnnotations.Schema;

namespace STAGGI_Budget_API.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        //With MS doc for One to One
        public string BUserId { get; set; }
        public BUser BUser { get; set; } = null!;



        // // With DataAnotation
        //[ForeignKey("BUserId")]
        //public BUser BUser { get; set; }

    }
}
