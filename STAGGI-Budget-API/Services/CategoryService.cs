using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;

namespace STAGGI_Budget_API.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
         _categoryRepository = categoryRepository;
        }

        public Result <List<CategoryDTO>> GetAll() 
        {
            var result = _categoryRepository.GetAll();

            var categoriesDTO = new List<CategoryDTO>();
            foreach (var category in result)
            {
                categoriesDTO.Add(new CategoryDTO 
                {
                    Name = category.Name,
                    ImageUrl = category.ImageUrl,
                });
            }
            return Result<List<CategoryDTO>>.Success(categoriesDTO);
        }


        public Result<CategoryDTO> CreateCategory()
        {
            throw new NotImplementedException();
        }

        public Result<CategoryDTO> DeleteCategory()
        {
            throw new NotImplementedException();
        }

        public Result<CategoryDTO> UpdateCategory()
        {
            throw new NotImplementedException();
        }
    }
}
