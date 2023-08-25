using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBUserRepository _buserRepository;

        public AccountService(IAccountRepository accountRepository, IBUserRepository buserRepository)
        {
            _accountRepository = accountRepository;
            _buserRepository = buserRepository;
        }

        public Result<List<AccountDTO>> GetAll()
        {
            //string userEmail = "gr@mail.com"; // TO DO: reemplazar cuando exista el servicio

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

        public Result<List<AccountDTO>> GetAccountsByBUser(string email)
        {
            BUser bUser = _buserRepository.FindByEmail(email);
            if (bUser == null)
            {
                throw new ApplicationException("no se encontró el usuario"); //TODO - Corregir
            }

            //string userEmail = "gr@mail.com"; // TO DO: reemplazar cuando exista el servicio

            var result = _accountRepository.GetAccountsByBUser(bUser.Id);

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
    }
}
