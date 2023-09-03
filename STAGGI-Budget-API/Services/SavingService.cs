using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Services
{
    public class SavingService : ISavingService
    {
        private readonly ISavingRepository _savingRepository;
        private readonly IBUserService _bUserService;

        public SavingService(ISavingRepository savingRepository, IBUserService bUserService)
        {
            _savingRepository = savingRepository;
            _bUserService = bUserService;
        }
        public Result<List<SavingDTO>> GetAll(string email)
        {
            var result = _savingRepository.GetAllByEmail(email);

            var savingsDTO = result.Select(s => new SavingDTO
            {
                Id = s.Id,
                Name = s.Name,
                Balance = s.Balance,
                TargetAmount = s.TargetAmount,
                DueDate = s.DueDate,
                IsDisabled = s.IsDisabled,
            }).ToList();

            return Result<List<SavingDTO>>.Success(savingsDTO);
        }

        public Result<SavingDTO> GetSavingById(int id)
        {
            var result = _savingRepository.GetById(id, true);

            var savingDTO = new SavingDTO
            {
                Id = result.Id,
                Name = result.Name,
                Balance = result.Balance,
                TargetAmount = result.TargetAmount,
                DueDate = result.DueDate,
                IsDisabled = result.IsDisabled,
                Transactions = result.Transactions.Select(tr => new TransactionDTO
                {
                    Id = tr.Id,
                    Title = tr.Title,
                    Description = tr.Description,
                    Amount = tr.Amount,
                    Type = tr.Type.ToString(),
                    CreateDate = DateTime.Now,
                    Category = new CategoryDTO
                    {
                        Id = tr.Category.Id,
                        Name = tr.Category.Name,
                        Image = tr.Category.Image
                    }
                }).ToList()
            };

            return Result<SavingDTO>.Success(savingDTO);
        }

        public Result<string> CreateSaving(RequestSavingDTO request, string email)
        {
            try
            {
                var user = _bUserService.GetByEmail(email);

                _savingRepository.Save(new Saving
                {
                    Name = request.Name,
                    TargetAmount = (double)request.TargetAmount,
                    DueDate = request.DueDate,
                    BUserId = user.Id
                });

                return Result<string>.Success("Created");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = ex.Message
                });
            }
        }

        public Result<string> UpdateSaving(int id, RequestSavingDTO request, string email)
        {
            try
            {
                Saving saving = _savingRepository.GetById(id);

                if (saving == null)
                {
                    return Result<string>.Failure(new ErrorResponseDTO
                    {
                        Status = 404,
                        Error = "Not Found",
                        Message = "Result not found"
                    });
                }

                if (request.Name != null) saving.Name = request.Name;
                if (request.TargetAmount is not null && request.TargetAmount != 0) saving.TargetAmount = (double)request.TargetAmount;
                if (request.DueDate.HasValue) saving.DueDate = request.DueDate;
                if (request.IsDisabled is not null && request.IsDisabled != saving.IsDisabled) saving.IsDisabled = request.IsDisabled.Value;

                _savingRepository.Save(saving);

                return Result<string>.Success("Updated");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = ex.Message
                });
            }
        }
    }
}
