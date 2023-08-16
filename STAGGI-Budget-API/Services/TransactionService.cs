using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
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

        public Result<List<TransactionDTO>> GetAll()
        { 
            var result = _transactionRepository.GetAll();
            var transactionsDTO = new List<TransactionDTO>();
            foreach (var transaction in result) 
            {
                transactionsDTO.Add(new TransactionDTO 
                { 
                    Title = transaction.Title,
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Type = transaction.Type,
                    CreateDate = DateTime.Now,
                    CategoryId = transaction.CategoryId,
                });
            }

            return Result<List<AccountDTO>>.Success(TransactionDTO);
        }

        public Result<TransactionDTO> CreateTransaction(TransactionDTO transactionDTO)
        {
            try
            {
                _transactionRepository.Save(new Transaction
                {
                    Title = transactionDTO.Title,
                    Description = transactionDTO.Description,
                    Amount = transactionDTO.Amount,
                    Type = transactionDTO.Type,
                    CreateDate = DateTime.Now,
                    CategoryId = transactionDTO.CategoryId,
                });

                return Result<TransactionDTO>.Success(transactionDTO);
            }
            catch
            {
                return Result<TransactionDTO>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = "No se pudo realizar la transacción."
                });
            }
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
            catch
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
