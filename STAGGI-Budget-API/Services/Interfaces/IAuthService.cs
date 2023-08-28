using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> Login(LoginRequestDTO request);
        Task<Result<string>> Register(RegisterRequestDTO request);
       // string GetEmailFromToken(string token);
    }
}
