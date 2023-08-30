using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;
using System.Text.RegularExpressions;

namespace STAGGI_Budget_API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryService _categoryService;
        private readonly IBUserService _bUserService;
        private readonly IBudgetService _budgetService;

        public TransactionService(ITransactionRepository transactionRepository, ICategoryService categoryService, IBUserService bUserService, IBudgetService budgetService)
        {
            _transactionRepository = transactionRepository;
            _categoryService = categoryService;
            _bUserService = bUserService;
            _budgetService = budgetService;
        }

        public Result<List<TransactionDTO>> GetAllByUserEmail(string userEmail)
        {
            var result = _transactionRepository.FindByUserEmail(userEmail);
            var transactionsDTO = new List<TransactionDTO>();

            foreach (var transaction in result)
            {
                transactionsDTO.Add(new TransactionDTO
                {
                    Title = transaction.Title,
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Type = (transaction.Type).ToString(),
                    CreateDate = DateTime.Now,
                    //CategoryId = transaction.CategoryId,
                });
            }

            return Result<List<TransactionDTO>>.Success(transactionsDTO);
        }

        public Result<string> CreateTransaction(RequestTransactionDTO transactionDTO, string email)
        {
            try
            {
                BUser user = _bUserService.GetByEmail(email);
                
                if (user == null)
                {
                    var newErrorResponse = new ErrorResponseDTO
                    {
                        Error = "Server Error",
                        Message = "El usuario no existe.",
                        Status = 500
                    };
                }

                var userBudget = _budgetService.GetAllByEmail(email);


                var userCategories = _categoryService.GetAllUserCategories(email);
                var categoryMatch = userCategories.FirstOrDefault(c => c.Name == transactionDTO.Category);

                _transactionRepository.Save(new Transaction
                {
                    Title = transactionDTO.Title,
                    Description = transactionDTO.Description,
                    Amount = (double)transactionDTO.Amount,
                    Type = (TransactionType)(transactionDTO.Type),
                    CreateDate = DateTime.Now,
                    AccountId = user.Account.Id,
                    CategoryId = categoryMatch.Id,
                    //Saving = ,
                    //Budget = ,
                });

                return Result<string>.Success("created");
            }
            catch
            {
                return Result<string>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = "No se pudo realizar la transacción."
                });
            }
        }
        public Result<TransactionDTO> ModifyTransaction(int transactionId, RequestTransactionDTO request)
        {
            try
            {
                Transaction existingTransaction = _transactionRepository.FindById(transactionId);
                
                if (existingTransaction == null)
                {
                    return Result<TransactionDTO>.Failure(new ErrorResponseDTO
                    {
                        Status = 404,
                        Error = "Not Found",
                        Message = "Result not found"
                    });
                }

                existingTransaction.Type = (TransactionType)(request.Type);
                existingTransaction.Title = request.Title;
                existingTransaction.Description = request.Description;
                existingTransaction.Amount = (double)request.Amount;
                existingTransaction.CreateDate = DateTime.Now;

                _transactionRepository.Save(existingTransaction);

                TransactionDTO updatedTransaction = new TransactionDTO
                {
                    Title = existingTransaction.Title,
                    Description = existingTransaction.Description,
                    Amount = existingTransaction.Amount,
                    Type = (existingTransaction.Type).ToString(),
                    CreateDate = existingTransaction.CreateDate
                };
                                                
                return Result<TransactionDTO>.Success(updatedTransaction);

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

        public Result<List<TransactionDTO>> SearchTransaction(string searchParameter, string email)
        {
            Regex regexName = new Regex("[a-zA-Z0-9]");

            if (searchParameter == null)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Error = "Server Error",
                    Message = "Usted no ingreso ningun dato a buscar.",
                    Status = 500
                };

                return Result<List<TransactionDTO>>.Failure(newErrorResponse);
            }
                       
            if (searchParameter.Length > 15)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Error = "Server Error",
                    Message = "La longitud de la busqueda supera el maximo de caracteres.",
                    Status = 500
                };

                return Result<List<TransactionDTO>>.Failure(newErrorResponse);
            }

            Match searchMatch = regexName.Match(searchParameter);
            if (!searchMatch.Success)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Error = "Server Error",
                    Message = "Usted no puede utilizar caracteres especiales para buscar.",
                    Status = 500
                };

                return Result<List<TransactionDTO>>.Failure(newErrorResponse);
            }

            var transactionSearch = _transactionRepository.Search(searchParameter, email);
            var transactionSearchDTO = new List<TransactionDTO>();
            foreach(Transaction transaction in transactionSearch)
            {
                TransactionDTO newTransactionSearchDTO = new TransactionDTO
                {
                    Title = transaction.Title,
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Type = transaction.Type.ToString(),
                    CreateDate = transaction.CreateDate,
                };

                transactionSearchDTO.Add(newTransactionSearchDTO);
            }

            if (transactionSearchDTO == null)
            {
                return Result<List<TransactionDTO>>.Failure(new ErrorResponseDTO
                {
                    Status = 204,
                    Error = "Error en la busqueda",
                    Message = "No se pudo encontrar la transaccion buscada."
                });
            }            

            return Result<List<TransactionDTO>>.Success(transactionSearchDTO);
        }

        public Result<TransactionDTO> GetTransactionById(int id)
        {
            var transaction = _transactionRepository.FindById(id);
            var type = transaction.Type;

            var transactionDTO = new TransactionDTO
            {
                Title = transaction.Title,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = (type).ToString(),
                CreateDate = transaction.CreateDate
            };

            return Result<TransactionDTO>.Success(transactionDTO);

        }

        /*public Result<string> DeleteTransactionById(int id)
        {
            var transaccionesDelUsuario = _transactionRepository.FindById(id);
            _transactionRepository.Delete(transaccionesDelUsuario);            

            return Result<string>.Success("deleted");
        }*/
    }
}
