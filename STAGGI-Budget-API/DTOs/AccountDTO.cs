namespace STAGGI_Budget_API.DTOs
{
    public class AccountDTO
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public bool IsPrincipal { get; set; }
        public string BUserId { get; set; }
    }
}
