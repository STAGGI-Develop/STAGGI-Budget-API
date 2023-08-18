using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ICategoryService
    {
        public Result<List<CategoryDTO>> GetAll();
        public Result<CategoryDTO> GetById(long id);
        public Result<CategoryDTO> CreateCategoryForCurrentClient();

        public Result<List<CategoryDTO>> GetCurrentClientCategory();
    }
}
