using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using System.Collections.Generic;

namespace STAGGI_Budget_API.Services
{
    public interface IAccountService
    {
        public Result<List<AccountDTO>> GetAll();
        public Result<AccountDTO> GetById(long id);
        public Result<AccountDTO> CreateAccountForCurrentClient();
        public Result<List<AccountDTO>> GetCurrentClientAccounts();
    }
}
