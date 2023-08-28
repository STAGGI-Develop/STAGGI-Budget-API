using STAGGI_Budget_API.Enums;
using Microsoft.AspNetCore.Identity;
using STAGGI_Budget_API.DTOs;
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
        public readonly ISubscriptionRepository _subscriptionRepository;

        public BUserService(IBUserRepository buserRepository, ISubscriptionRepository subscriptionRepository)
        {
            _buserRepository = buserRepository;
            _subscriptionRepository = subscriptionRepository;
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

        public Result<RegisterRequestDTO> RegisterBUser(UserManager<BUser> _userManager)
        {
            var registerRequestDTO = new RegisterRequestDTO();

            var newUser = new RegisterRequestDTO
            {
                FirstName = registerRequestDTO.FirstName,
                LastName = registerRequestDTO.LastName,
                Email = registerRequestDTO.Email,
                Password = registerRequestDTO.Password,
            };

            var finalBUser = new BUser
            {
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.Email
            };
            _userManager.CreateAsync(finalBUser, newUser.Password);
            _userManager.AddToRoleAsync(finalBUser, "User");

            _buserRepository.Save(finalBUser);

            return Result<RegisterRequestDTO>.Success(newUser);
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
        
        public Result<UserProfileDTO> GetUserProfile(string email)
        {
            BUser user = GetByEmail(email);

            var userProfileDTO = new UserProfileDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

                SuscriptionState = user.Subscription?.IsActive == true ? "Premium" : "Classic",
                EndDate = user.Subscription?.EndDate
                
            };

            return Result<UserProfileDTO>.Success(userProfileDTO);

        }

        public Result<RegisterRequestDTO> RegisterBUser(RegisterRequestDTO registerRequestDTO, UserManager<BUser> _userManager)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Subscribe(string userEmail)
        {
            BUser user = GetByEmail(userEmail);

            Subscription subscription = new Subscription()
            {
                IsActive = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                BUser = user,                          //Arreglar por cambio de modelos
            };

            _subscriptionRepository.Save(subscription);

            return Result<bool>.Success(true);
        }
    }
}
