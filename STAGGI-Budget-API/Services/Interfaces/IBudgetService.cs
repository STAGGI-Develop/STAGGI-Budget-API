using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface IBudgetService
    {
        
        public Result<List<BudgetDTO>> GetAllByEmail(string email);
        public Result<BudgetDTO> GetById(long id);
        public Result<string> CreateBudget(RequestBudgetDTO budgetDTO, string email);
        //public Result<string> DeleteBudget(long budgetId);
        public Result<BudgetDTO> UpdateBudget(int budgetId, RequestBudgetDTO updatedBudgetDTO, string email);

        //public Result<List<BudgetDTO>> GetCurrentClientBudgets();
     

    }
}
