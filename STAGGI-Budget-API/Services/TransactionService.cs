using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
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
        private readonly IBUserRepository _bUserRepository;

        public TransactionService(ITransactionRepository transactionRepository, IBUserRepository bUserRepository)
        {
            _transactionRepository = transactionRepository;
            _bUserRepository = bUserRepository;
        }

        public Result<List<TransactionDTO>> GetAllByUserEmail(string userEmail)
        { 
            var result = _transactionRepository.FindByUserEmail(userEmail);
            var transactionsDTO = new List<TransactionDTO>();

            foreach (var transaction in result) 
            {
                transactionsDTO.Add(new TransactionDTO 
                {
                    Id = transaction.Id,
                    Title = transaction.Title,
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Type = transaction.Type,
                    CreateDate = DateTime.Now,
                    //CategoryId = transaction.CategoryId,
                });
            }

            return Result<List<TransactionDTO>>.Success(transactionsDTO);
        }

        public Result<TransactionDTO> CreateTransaction(CreateTransactionDTO transactionDTO, string currentEmail)
        {
            try
            {
                //Tengo que pasarme el email para buscar el usuario y sacarle el account completo,
                //El budget, el saving y la categoria tengo que buscar que coincidan con el nombre filtrandolo ver linea 161 a 176 del dbinitializer

                BUser user = _bUserRepository.FindByEmail(currentEmail);
                if (user == null)
                {
                    throw new ApplicationException("no se encontró el usuario"); 
                }
                               
                var categoryMatch =  user.Categories.FirstOrDefault(c => c.Name == transactionDTO.Category);
                
                
                var budgetMatch = user.Budgets.FirstOrDefault(b => b.Name == transactionDTO.Saving);
                //var savingMatch = user.Savings.FirstOrDefault(s => s.Category.Name == transactionDTO.Category);

                var newTransaction = new Transaction
                {
                    Title = transactionDTO.Title,
                    Description = transactionDTO.Description,
                    Amount = transactionDTO.Amount,
                    Type = (TransactionType)transactionDTO.Type,
                    CreateDate = DateTime.Now,
                    Account = user.Account,
                    Budget = budgetMatch,
                    Category = categoryMatch,
                    //Saving = savingMatch
                };

                _transactionRepository.Save(newTransaction);

                var newTransactionDTO = new TransactionDTO
                {
                    Title = newTransaction.Title,
                    Description = newTransaction.Description,
                    Amount = newTransaction.Amount,
                    Type = (TransactionType)newTransaction.Type,
                    CreateDate = DateTime.Now,
                    //CategoryId = transactionDTO.CategoryId,
                };

                return Result<TransactionDTO>.Success(newTransactionDTO);
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
                //transaction.CategoryId = transactionDTO.CategoryId;

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

        public Result<List<TransactionDTO>> SearchTransaction(string searchParameter, string userEmail)
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

            var transactionSearch = _transactionRepository.Search(searchParameter, userEmail);
            var transactionSearchDTO = new List<TransactionDTO>();
            foreach(Transaction transaction in transactionSearch)
            {
                TransactionDTO newTransactionSearchDTO = new TransactionDTO
                {
                    Id = transaction.Id,
                    Title = transaction.Title,
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Type = transaction.Type,
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

        public Result<TransactionDTO> GetTransactionById(long id)
        {
            var transaction = _transactionRepository.FindById(id);

            var transactionDTO = new TransactionDTO
            {
                Id = transaction.Id,
                Title = transaction.Title,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                CreateDate = transaction.CreateDate,
                Category = new CategoryDTO
                {
                    Id = transaction.Category.Id,
                    Name = transaction.Category.Name,
                    ImageUrl = transaction.Category.ImageUrl
                },
                /*Saving = new SavingDTO
                {
                    Name = transaction.Saving.Name,
                    Balance = transaction.Saving.Balance,
                    TargetAmount = transaction.Saving.TargetAmount,
                    DueDate = transaction.Saving.DueDate
                },
                Budget = new BudgetDTO
                {
                    Name = transaction.Budget.Name,
                    LimitAmount = transaction.Budget.LimitAmount,
                    Period = transaction.Budget.Period,
                    Balance = transaction.Budget.Balance
                }*/
            };

            return Result<TransactionDTO>.Success(transactionDTO);
        }
    }
}
