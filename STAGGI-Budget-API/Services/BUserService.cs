using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Services
{
    public class BUserService : IBUserService
    {
        private readonly IBUserRepository _buserRepository;

        public BUserService(IBUserRepository buserRepository)
        {
            _buserRepository = buserRepository;
        }
        public Result<List<BUserDTO>> GetAll()
        {
            var result = _buserRepository.GetAll();

            var usersDTO = new List<BUserDTO>();
            foreach (var user in result)
            {
                usersDTO.Add(new BUserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsPremium = user.IsPremium,

                });
            }

            return Result<List<BUserDTO>>.Success(usersDTO);
        }

        public Result<BUserDTO> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public BUser GetByEmail(string email)
        {
            return _buserRepository.FindByEmail(email);
        }

        public Result<BUserDTO> CreateAccountForCurrentClient()
        {
            throw new NotImplementedException();
        }

        public Result<List<BUserDTO>> GetCurrentClientAccounts()
        {
            throw new NotImplementedException();
        }

        public Result<ProfileDTO> GetProfile(string email)
        {
            var userProfile = _buserRepository.UserProfile(email);

            ProfileDTO profile = new()
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Email = userProfile.Email,
                IsPremium = userProfile.IsPremium,
                SubscriptionEnd = userProfile.Subscription.EndDate.ToString(),
                Account = new AccountDTO
                {
                    Name = "",
                    Balance = userProfile.Account.Balance,
                    IsPrincipal = false,
                    BUserId = ""
                },
                Budgets = userProfile.Budgets.Select(b => new BudgetDTO
                {
                    Name = b.Name,
                    Balance = b.Balance,
                    LimitAmount = b.LimitAmount,
                }).ToList(),
                Savings = userProfile.Savings.Select(s => new SavingDTO
                {
                    Name = s.Name,
                    Balance = s.Balance,
                    TargetAmount = s.TargetAmount,
                }).ToList(),
                Transactions = userProfile.Account.Transactions.Select(t => new TransactionDTO
                {
                    Amount = t.Amount,
                    //Type = t.Type == 0 ? "Income" : "Expense"
                }).ToList()
            };

            return Result<ProfileDTO>.Success(profile);
        }
    }
}
