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
        private readonly UserManager<BUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public BUserService(IBUserRepository buserRepository,
            ISubscriptionRepository subscriptionRepository,
            UserManager<BUser> userManager)
        {
            _buserRepository = buserRepository;
            _subscriptionRepository = subscriptionRepository;
            _userManager = userManager;
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

        public async Task<Result<string>> RegisterUserAsync(RequestUserDTO request)
        {
            try
            {
                var finalBUser = new BUser
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.Email,
                    Account = new Account()
                };
                finalBUser.Account.BUserId = finalBUser.Id;

                await _userManager.CreateAsync(finalBUser, request.Password);
                await _userManager.AddToRoleAsync(finalBUser, "User");

                return Result<string>.Success(finalBUser.Id);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "",
                    Message = ex.Message
                });
            }
        }

        public BUser GetByEmail(string email)
        {
            return _buserRepository.FindByEmail(email);
        }

        public Result<UserProfileDTO> GetProfile(string email)
        {
            var userProfile = _buserRepository.UserProfile(email);

            UserProfileDTO profile = new UserProfileDTO
            {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Email = userProfile.Email,
                IsPremium = userProfile.IsPremium,
                Balance = userProfile.Account.Balance,
                Subscription = new SubscriptionDTO
                {
                    IsActive = userProfile.Subscription.IsActive,
                    StartDate = userProfile.Subscription.StartDate,
                    EndDate = userProfile.Subscription.EndDate,
                },
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
                    Type = t.Type == 0 ? "Income" : "Expense"
                }).ToList()
            };

            return Result<UserProfileDTO>.Success(profile);
        }

        public Result<bool> Subscription(string userEmail, bool status)
        {
            try
            {
                BUser user = GetByEmail(userEmail);

                user.Subscription.IsActive = status;
                if (status)
                {
                    user.Subscription.StartDate = DateTime.Now;
                    user.Subscription.EndDate = DateTime.Now.AddMonths(1);
                }

                //_buserRepository.Save(user);
                _subscriptionRepository.Save(user.Subscription);

                return Result<bool>.Success(status);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "",
                    Message = ex.Message
                });
            }
        }
    }
}
