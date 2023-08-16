using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services
{
    public interface IBudgetService
    {
        public Result<List<BudgetDTO>> GetAll();
        public Result<BudgetDTO> GetById(long id);
        public Result<BudgetDTO> CreateBudgetForCurrentClient();

        public Result<List<BudgetDTO>> GetCurrentClientBudgets();
    }
}
