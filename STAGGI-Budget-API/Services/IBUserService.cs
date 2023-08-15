using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using System.Collections.Generic;

namespace STAGGI_Budget_API.Services
{
    public interface IBUserService
    {
        public Result<List<BUserDTO>> GetAll();
        public Result<BUserDTO> GetById(long id);
        public Result<BUserDTO> CreateAccountForCurrentClient();
        public Result<List<BUserDTO>> GetCurrentClientAccounts();
    }
}
