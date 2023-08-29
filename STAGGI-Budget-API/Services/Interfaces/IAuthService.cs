﻿using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> Login(RequestLoginDTO request);
        Task<Result<string>> Register(RequestUserDTO request);

        string ValidateToken(string token); 

    }
}
