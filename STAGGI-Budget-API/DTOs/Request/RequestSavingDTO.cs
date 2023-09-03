namespace STAGGI_Budget_API.DTOs.Request
{
    public class RequestSavingDTO
    {
        public string? Name { get; set; }
        public double? TargetAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
