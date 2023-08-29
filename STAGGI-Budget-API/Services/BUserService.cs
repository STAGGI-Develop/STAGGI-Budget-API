using STAGGI_Budget_API.Enums;
using Microsoft.AspNetCore.Identity;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;
using STAGGI_Budget_API.DTOs.Request;

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
        public Result<List<UserProfileDTO>> GetAll()
        {
            var result = _buserRepository.GetAll();

            var usersDTO = new List<UserProfileDTO>();
            foreach (var user in result)
            {
                usersDTO.Add(new UserProfileDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsPremium = user.IsPremium,

                });
            }

            return Result<List<UserProfileDTO>>.Success(usersDTO);
        }

        public Result<UserProfileDTO> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Result<RequestUserDTO> RegisterBUser(UserManager<BUser> _userManager)
        {
            var registerRequestDTO = new RequestUserDTO();

            var newUser = new RequestUserDTO
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

            return Result<RequestUserDTO>.Success(newUser);
        }

        public BUser GetByEmail(string email)
        {
            return _buserRepository.FindByEmail(email);
        }

        public Result<UserProfileDTO> CreateAccountForCurrentClient()
        {
            throw new NotImplementedException();
        }

        public Result<List<UserProfileDTO>> GetCurrentClientAccounts()
        {
            throw new NotImplementedException();
        }

        public Result<UserProfileDTO> GetProfile(string email)
        {
            var userProfile = _buserRepository.UserProfile(email);

            UserProfileDTO profile = new()
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Email = userProfile.Email,
                IsPremium = userProfile.IsPremium,
                Subscription = new SubscriptionDTO
                {
                    IsActive = userProfile.Subscription.IsActive,
                    StartDate = (DateTime)userProfile.Subscription.StartDate,
                    EndDate = (DateTime)userProfile.Subscription.EndDate,
                },
                Balance = userProfile.Account.Balance,
                Budgets = userProfile.Budgets.Select(b => new BudgetDTO
                {
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

            return Result<UserProfileDTO>.Success(profile);
        }
        
        //public Result<UserProfileDTO> GetUserProfile(string email)
        //{
        //    BUser user = GetByEmail(email);

        //    var userProfileDTO = new UserProfileDTO
        //    {
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        PhoneNumber = user.PhoneNumber,

        //        SuscriptionState = user.Subscription?.IsActive == true ? "Premium" : "Classic",
        //        EndDate = user.Subscription?.EndDate
                
        //    };

        //    return Result<UserProfileDTO>.Success(userProfileDTO);

        //}

        public Result<RequestUserDTO> RegisterBUser(RequestUserDTO registerRequestDTO, UserManager<BUser> _userManager)
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
