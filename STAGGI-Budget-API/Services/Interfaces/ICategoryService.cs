using Microsoft.AspNetCore.Mvc;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ICategoryService
    {
        public Result<List<CategoryDTO>> GetAll();
        public Result<CategoryDTO> FindById(long id);
        public Result<string> CreateCategory(CategoryDTO categoryDTO);
        public Result<string> UpdateCategory(long Id, CategoryDTO categoryDTO);
    }
}
