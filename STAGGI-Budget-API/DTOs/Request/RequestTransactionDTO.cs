using STAGGI_Budget_API.Enums;

namespace STAGGI_Budget_API.DTOs.Request
{
    public class RequestTransactionDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double? Amount { get; set; }
        public int Type { get; set; }   //  0: OUT - 1: IN
        public string? CreateDate { get; set; }
        public string Category { get; set; }
        public string? Saving { get; set; }
        public string? Budget { get; set; }
    }
}
