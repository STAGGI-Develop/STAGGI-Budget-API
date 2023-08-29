using Microsoft.AspNetCore.Identity;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using System.Collections.Generic;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface IBUserService
    {
        public Result<List<UserProfileDTO>> GetAll();
        //public Result<UserProfileDTO> GetById(long id);
        public Task<Result<string>> RegisterUserAsync(RequestUserDTO request);
        public BUser GetByEmail(string email);
        public Result<UserProfileDTO> GetProfile( string email);
        public Result<bool> Subscription(string userEmail, bool status);
    }
}
