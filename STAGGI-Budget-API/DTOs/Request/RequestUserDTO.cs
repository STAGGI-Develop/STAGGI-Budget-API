namespace STAGGI_Budget_API.DTOs.Request
{
    public class RequestUserDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
