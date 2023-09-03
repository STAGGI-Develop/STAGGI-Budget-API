namespace STAGGI_Budget_API.DTOs.Request
{
    public class RequestBudgetDTO
    {
        public double? LimitAmount { get; set; }
        public string? Category { get; set; }
        public int? Period { get; set; }
        public bool? IsDisabled { get; set; }

    }
}
