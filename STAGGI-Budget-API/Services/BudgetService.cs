using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Services
{
    public class BudgetService: IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public Result<BudgetDTO> CreateBudgetForCurrentClient()
        {
            throw new NotImplementedException();
        }

        public Result <List<BudgetDTO>>GetAll()
        {
            //var result = _budgetRepository.GetAll();

            //var budgetsDTO = new List<BudgetDTO>();
            //foreach (var budget in result)
            //{
            //    budgetsDTO.Add(new BudgetDTO
            //    {
            //        Name = budget.Name,

            //    });
                
            //}
            throw new NotImplementedException();
        }

        public Result<BudgetDTO> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Result<List<BudgetDTO>> GetCurrentClientBudgets()
        {
            throw new NotImplementedException();
        }
    }
   

}
