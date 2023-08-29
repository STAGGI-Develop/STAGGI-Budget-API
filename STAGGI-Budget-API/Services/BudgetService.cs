using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace STAGGI_Budget_API.Services
{

    public class BudgetService: IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        
        //constructor
        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        //getAll


        public Result<List<BudgetDTO>> GetAllByEmail(string email)
        {

            var result = _budgetRepository.GetAllByEmail(email);
            var budgetDTOs = new List<BudgetDTO>();

            foreach (var budget in result)
            {
                budgetDTOs.Add(new BudgetDTO
                {
                    Name = budget.Name,
                    LimitAmount = budget.LimitAmount,
                    Period = budget.Period,
                   
                });
            }

            return Result<List<BudgetDTO>>.Success(budgetDTOs);
        }


        //GetById
        public Result<BudgetDTO> GetById (long id)
        {
            var result = _budgetRepository.GetById(id);
            if (result == null)
            {
                return Result<BudgetDTO>.Failure(new ErrorResponseDTO
                {
                    Status = 204,
                    Error = "No Content",
                    Message = "El presupuesto no fue encontrado."
                });
            }
            var budgetDTO = new BudgetDTO
            {
                Name = result.Name,
                LimitAmount = result.LimitAmount,
                Period = result.Period,
                Balance = result.Balance,
                Category = new Category
                {
                    Name = result.Category.Name,
                    ImageUrl = result.Category.ImageUrl,
                }
            };
            return Result<BudgetDTO>.Success(budgetDTO);
        }
       
        //CreateBudget

        public Result<BudgetDTO> CreateBudget(BudgetDTO budgetDTO)
        {
            try
            {
                _budgetRepository.Save(new Budget
                {
                    Name = budgetDTO.Name,
                    LimitAmount = budgetDTO.LimitAmount,
                    Period = budgetDTO.Period,               
                });

                return Result<BudgetDTO>.Success(budgetDTO);
            }
            catch
            {
                return Result<BudgetDTO>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = "No se pudo realizar la transacción."
                });
            }
        }
        //DeleteBudget
        public Result<Budget> DeleteBudget(long budgetId)
        {
            try
            {
                // obtenemos presupuesto a eliminar utilizando el budgetId.
                Budget budgetToDelete = _budgetRepository.GetById(budgetId);

                if (budgetToDelete != null)
                {
                    _budgetRepository.Delete(budgetToDelete);

                }
                return Result<Budget>.Success(budgetToDelete)

             ;
            }catch
           
            {
                return Result<Budget>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = "No se pudo realizar la acción."
                });
            }
        }

        //UpdateBudget
        public Result<BudgetDTO> UpdateBudget(int budgetId, BudgetDTO updatedBudgetDTO, string email)
        {
            try
            {
                Budget existingBudget = _budgetRepository.GetById(budgetId);

                if (existingBudget == null)
                {
                    return Result<BudgetDTO>.Failure(new ErrorResponseDTO
                    {
                        Status = 404,
                        Error = "Not Found",
                        Message = "El presupuesto no fue encontrado."
                    });
                }

                
                existingBudget.Name = updatedBudgetDTO.Name;
                existingBudget.LimitAmount = updatedBudgetDTO.LimitAmount;
                existingBudget.Period = updatedBudgetDTO.Period;
               

                
                _budgetRepository.Save(existingBudget);

                
                BudgetDTO updatedBudget = new BudgetDTO
                {
                    Name = existingBudget.Name,
                    LimitAmount = existingBudget.LimitAmount,
                    Period = existingBudget.Period,
                    
                };

                return Result<BudgetDTO>.Success(updatedBudget);
            }
            catch
            {
                return Result<BudgetDTO>.Failure(new ErrorResponseDTO
                {
                    Status = 500,
                    Error = "Internal Server Error",
                    Message = "No se pudo realizar la acción."
                });
            }
        }

    }
    }
   


