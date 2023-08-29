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
        public Result<UserProfileDTO> GetById(long id);
        public BUser GetByEmail(string email);
        public Result<UserProfileDTO> CreateAccountForCurrentClient();
        public Result<List<UserProfileDTO>> GetCurrentClientAccounts();
        public Result<UserProfileDTO> GetProfile( string email);
        public Result<RequestUserDTO> RegisterBUser(RequestUserDTO registerRequestDTO, UserManager<BUser> _userManager);
        //public Result<UserProfileDTO> GetUserProfile(string email);
        public Result<bool> Subscribe(string userEmail);
    }
}
