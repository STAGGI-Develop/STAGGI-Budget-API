using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;

namespace STAGGI_Budget_API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public Result<TransactionDTO> ModifyTransaction(long transactionId, TransactionDTO transactionDTO)
        {
            try
            {
                var transaction = _transactionRepository.FindById(transactionId) ?? throw new KeyNotFoundException($"Article with id: {transactionId} not found");

                transaction.Title = transactionDTO.Title;
                transaction.Description = transactionDTO.Description;
                transaction.Amount = transactionDTO.Amount;
                transaction.Type = transactionDTO.Type;
                transaction.CategoryId = transactionDTO.CategoryId;

                return Result<TransactionDTO>.Success(transactionDTO);

            }
            catch (Exception ex)
            {
                return Result<TransactionDTO>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = "No se pudo actualizar la transacción."
                });
            }
        }
    }
}
