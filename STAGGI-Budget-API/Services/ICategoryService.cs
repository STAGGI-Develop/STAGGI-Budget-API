using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services
{
    public interface ICategoryService
    {
        public Result<List<CategoryDTO>> GetAll();
        public Result<CategoryDTO> DeleteCategory();
        public Result<CategoryDTO> UpdateCategory();
        public Result<CategoryDTO> CreateCategory();
    }
}
