namespace STAGGI_Budget_API.DTOs
{
    public class UserProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } //Así cuando lo modifiquen se cambie el email. sino vamos a andar dando vueltas
        public string? PhoneNumber { get; set; }
        public string? SuscriptionState { get; set; } //0: Classic || 1:Premium
        public DateTime? EndDate { get; set; }
    }
}
