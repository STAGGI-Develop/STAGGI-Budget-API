namespace STAGGI_Budget_API.Models
{
    public class Budget
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public double LimitAmount { get; set; }
        public int Period { get; set; }
        public int BUserId { get; set; }
        public BUser BUser { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
