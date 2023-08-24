using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;
using System.Text.RegularExpressions;

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

        public Result<CategoryDTO> FindById(long id)
        {
            var category = _categoryRepository.FindById(id);

            var categoryDTO = new CategoryDTO
            {
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };

            return Result<CategoryDTO>.Success(categoryDTO);
        }

        public Result<string> CreateCategory(CategoryDTO categoryDTO)
        {
            Regex regexName = new Regex("[a-zA-Z ]");
            Match categoryMatch = regexName.Match(categoryDTO.Name);

            Category newCategory = new Category
            {
                Name = categoryDTO.Name,
                ImageUrl = categoryDTO.ImageUrl,
            };

            if (categoryDTO.Name.Length > 15)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Error = "Server Error",
                    Message = "La longitud de la categoria supera el maximo",
                    Status = 500
                };
                               
                return Result<string>.Failure(newErrorResponse);
            }

            if (!categoryMatch.Success)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Error = "Server Error",
                    Message = "La categoria solo acepta letras",
                    Status = 500
                };

                return Result<string>.Failure(newErrorResponse);
            }

            _categoryRepository.Save(newCategory);

            return Result<string>.Success("Creacion exitosa");
        }

        public Result<string> UpdateCategory(long id, CategoryDTO categoryDTO)
        {
            var category = _categoryRepository.FindById(id);

            if (category == null)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Status = 404,
                    Error = "Not found",
                    Message = "No se encontró la categoría"
                };
                return Result<string>.Failure(newErrorResponse);
            }

            Regex regexName = new Regex("[a-zA-Z ]");
            Match categoryMatch = regexName.Match(categoryDTO.Name);

            if (categoryDTO.Name == null || categoryDTO.ImageUrl == null)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Status = 400,
                    Error = "Bad Request",
                    Message = "No se encontró el nombre o la imagen"
                };

                return Result<string>.Failure(newErrorResponse);
            }

            if (!categoryMatch.Success)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Error = "Internal Server Error",
                    Message = "La categoria solo acepta letras",
                    Status = 500
                };

                return Result<string>.Failure(newErrorResponse);
            }

            category.Name = categoryDTO.Name;
            category.ImageUrl = categoryDTO.ImageUrl;

            _categoryRepository.Save(category);

            return Result<string>.Success("Actualización exitosa");
        }

        public Result<string> DeleteCategory(long id)
        {
            var category = _categoryRepository.FindById(id);

            if (category == null)
            {
                var newErrorResponse = new ErrorResponseDTO
                {
                    Status = 404,
                    Error = "Not Found",
                    Message = "No se encontró la categoría"
                };

                return Result<string>.Failure(newErrorResponse);
            }

            _categoryRepository.Delete(id);
            return Result<string>.Success("");
        }
    }
}
