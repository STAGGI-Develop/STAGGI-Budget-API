using System.Security.Principal;

namespace STAGGI_Budget_API.DTOs
{
    public class BUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsPremium { get; set; }
        public ICollection<AccountDTO> Accounts { get; set; }
    }
}
