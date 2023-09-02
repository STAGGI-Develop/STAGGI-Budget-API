using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace STAGGI_Budget_API.Services
{

    public class BudgetService: IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryService _categoryService;
        private readonly IBUserService _bUserService;
        
        //constructor
        public BudgetService(IBudgetRepository budgetRepository, ICategoryService categoryService, IBUserService bUserService)
        {
            _budgetRepository = budgetRepository;
            _categoryService = categoryService;
            _bUserService = bUserService;
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
                    Id = budget.Id,
                    Balance = budget.Balance,
                    LimitAmount = budget.LimitAmount,
                    Period = budget.Period == 0 ? "Weekly" : "Monthly",
                    Category = new CategoryDTO
                    {
                        Name = budget.Category.Name,
                        Image = budget.Category.Image,
                        IsDisabled = budget.Category.IsDisabled,
                    }
                   
                });
            }

            return Result<List<BudgetDTO>>.Success(budgetDTOs);
        }


        //GetById
        public Result<BudgetDTO> GetById (int id)
        {
            var result = _budgetRepository.GetById(id, true);
            if (result == null)
            {
                return Result<BudgetDTO>.Failure(new ErrorResponseDTO
                {
                    Status = 204,
                    Error = "No Content",
                    Message = "Result not found"
                });
            }
            var budgetDTO = new BudgetDTO
            {
                Id = result.Id,
                LimitAmount = result.LimitAmount,
                Period = result.Period == 0 ? "Weekly" : "Monthly",
                Balance = result.Balance,
                Category = new CategoryDTO
                {
                    Id = result.Category.Id,
                    Name = result.Category.Name,
                    Image = result.Category.Image,
                    IsDisabled = result.Category.IsDisabled,
                },
                Transactions = result.Transactions.Select(tr => new TransactionDTO
                {
                    Id = tr.Id,
                    Title = tr.Title,
                    Description = tr.Description,
                    Amount = tr.Amount,
                    Type = tr.Type.ToString(),
                    CreateDate = tr.CreateDate,
                    Category = new CategoryDTO { 
                        Id = tr.Category.Id,
                        Name = tr.Category.Name,
                        Image = tr.Category.Image,
                        IsDisabled = tr.Category.IsDisabled,
                    }
                }).ToList()
            };
            return Result<BudgetDTO>.Success(budgetDTO);
        }
       
        //CreateBudget

        public Result<string> CreateBudget(RequestBudgetDTO request, string email)
        {
            try
            {
                var user = _bUserService.GetByEmail(email);
                var userCategories = _categoryService.GetAllWithBudgets(email);
                var categoryMatch = userCategories.FirstOrDefault(c => c.Name == request.Category);

                if (request.Category is not null && categoryMatch is null)
                {
                    return Result<string>.Failure(new ErrorResponseDTO
                    {
                        Status = 404,
                        Error = "Not Found",
                        Message = "Selected category was not found"
                    });
                }

                if (categoryMatch?.Budgets is not null && (
                    request.Period == (int)BudgetPeriod.Weekly && categoryMatch.Budgets.Any(bud => bud.Period == BudgetPeriod.Weekly) ||
                    request.Period == (int)BudgetPeriod.Monthly) && categoryMatch.Budgets.Any(bud => bud.Period == BudgetPeriod.Monthly ||
                    request.Period == (int)BudgetPeriod.Yearly && categoryMatch.Budgets.Any(bud => bud.Period == BudgetPeriod.Yearly
                    ))
                )
                {
                    return Result<string>.Failure(new ErrorResponseDTO
                    {
                        Status = 400,
                        Error = "Bad Request",
                        Message = "You can only have one budget per period in a category"
                    });
                }

                _budgetRepository.Save(new Budget
                {
                    LimitAmount = (double)request.LimitAmount,
                    Period = (BudgetPeriod)(request.Period),
                    CategoryId = categoryMatch.Id,
                    BUserId = user.Id
                });

                return Result<string>.Success("Created");
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

        //DeleteBudget
        //public Result<string> DeleteBudget(long budgetId)
        //{
        //    try
        //    {
        //        // obtenemos presupuesto a eliminar utilizando el budgetId.
        //        Budget budgetToDelete = _budgetRepository.GetById(budgetId);

        //        if (budgetToDelete != null)
        //        {
        //            _budgetRepository.Delete(budgetToDelete);

        //        }
        //        return Result<string>.Success("Deleted")

        //     ;
        //    }catch
           
        //    {
        //        return Result<string>.Failure(new ErrorResponseDTO
        //        {
        //            Status = 500,
        //            Error = "Internal Server Error",
        //            Message = "No se pudo realizar la acción."
        //        });
        //    }
        //}

        //UpdateBudget
        public Result<BudgetDTO> UpdateBudget(int budgetId, RequestBudgetDTO request, string email)
        {
            try
            {
                Budget existingBudget = _budgetRepository.GetById(budgetId);

                //TODO: si request trae categoría, buscar Category por nombre y sacar el id

                if (existingBudget == null)
                {
                    return Result<BudgetDTO>.Failure(new ErrorResponseDTO
                    {
                        Status = 404,
                        Error = "Not Found",
                        Message = "Result not found"
                    });
                }

                if (request.LimitAmount != null) existingBudget.LimitAmount = (double)request.LimitAmount;

                if (request.Period != null) existingBudget.Period = (BudgetPeriod)request.Period;

                //existingBudget.CategoryId = request.Category;

                _budgetRepository.Save(existingBudget);

                BudgetDTO updatedBudget = new BudgetDTO
                {
                    LimitAmount = existingBudget.LimitAmount,
                    Period = existingBudget.Period == 0 ? "Weekly" : "Monthly",
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

        public void UpdateBudgetBalance(int budgetId)
        {
            var budget = _budgetRepository.GetById(budgetId, true);
            if (budget != null)
            {
                switch (budget.Period)
                {
                    case (BudgetPeriod)0:
                        DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                        DateTime endOfWeek = startOfWeek.AddDays(7);

                        var weekTr = budget.Transactions.Where(t => t.CreateDate >= startOfWeek && t.CreateDate < endOfWeek);

                        budget.Balance = 0;
                        weekTr.ToList().ForEach(t => budget.Balance += t.Amount);

                        _budgetRepository.Save(budget);

                        break;

                    case (BudgetPeriod)1:
                        DateTime startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        DateTime endOfMonth = startOfMonth.AddMonths(1);

                        var monthTr = budget.Transactions.Where(t => t.CreateDate >= startOfMonth && t.CreateDate < endOfMonth);

                        budget.Balance = 0;
                        monthTr.ToList().ForEach(t => budget.Balance += t.Amount);

                        _budgetRepository.Save(budget);

                        break;

                    case (BudgetPeriod)2:
                        break;

                    default:
                        break;
                }
            }
        }


    }
}
