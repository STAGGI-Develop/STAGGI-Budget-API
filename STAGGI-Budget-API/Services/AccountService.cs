using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Result<List<AccountDTO>> GetAll()
        {
            string userEmail = "gr@mail.com"; // TO DO: reemplazar cuando exista el servicio

            var result = _accountRepository.GetAll();

            var accountsDTO = new List<AccountDTO>();
            foreach (var account in result)
            {
                accountsDTO.Add(new AccountDTO
                { 
                    Balance = account.Balance,
                    //BUserId = account.BUserId,
                });
            }

            return Result<List<AccountDTO>>.Success(accountsDTO);
        }

        public Result<AccountDTO> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Result<AccountDTO> CreateAccountForCurrentClient()
        {
            throw new NotImplementedException();
        }

        public Result<List<AccountDTO>> GetCurrentClientAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
