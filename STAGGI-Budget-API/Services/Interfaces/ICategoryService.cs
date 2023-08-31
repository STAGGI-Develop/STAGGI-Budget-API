using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ICategoryService
    {
        public Result<List<CategoryDTO>> GetAll();
        public Result<List<CategoryDTO>> GetByUserEmail(string email);
        public Result<List<CategoryExpenseDTO>> GetWithTransactions(string email, CategoryPeriod period);

        public Result<CategoryDTO> FindById(int id);
        public Result<string> CreateCategory(CategoryDTO categoryDTO, string email);
        public Result<string> UpdateCategory(int Id, CategoryDTO categoryDTO, string email);
        public List<Category> GetAllUserCategories(string email);
    }
}
