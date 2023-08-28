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
        private readonly IBUserService _buserService;
        public CategoryService(ICategoryRepository categoryRepository, IBUserService buserService)
        {
            _categoryRepository = categoryRepository;
            _buserService = buserService;
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
                    IsDisabled = category.IsDisabled,
                });
            }
            return Result<List<CategoryDTO>>.Success(categoriesDTO);
        }

        public Result <List<CategoryDTO>> GetByUserEmail(string email)
        {
            var result = _categoryRepository.GetAllByUserEmail(email);

            var categoriesDTO = new List<CategoryDTO>();
            foreach (var category in result)
            {
                categoriesDTO.Add(new CategoryDTO 
                { 
                    Name = category.Name,
                    ImageUrl = category.ImageUrl,
                    IsDisabled = category.IsDisabled,
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
                ImageUrl = category.ImageUrl,
                IsDisabled = category.IsDisabled
            };

            return Result<CategoryDTO>.Success(categoryDTO);
        }

        public Result<string> CreateCategory(CategoryDTO categoryDTO, string email)
        {
            Regex regexName = new Regex("[a-zA-Z ]");
            Match categoryMatch = regexName.Match(categoryDTO.Name);

            BUser user = _buserService.GetByEmail(email);

            if (user == null)
            {
                var errorResponse = new ErrorResponseDTO
                {
                    Status = 404,
                    Error = "Not found",
                    Message = "No se encontró el usuario"
                };
                return Result<string>.Failure(errorResponse);
            }

            Category newCategory = new Category
            {
                Name = categoryDTO.Name,
                ImageUrl = categoryDTO.ImageUrl,
                BUser = user
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

        public Result<string> UpdateCategory(long id, CategoryDTO categoryDTO, string email)
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


            if (categoryDTO.Name != null)
            {
                Regex regexName = new Regex("[a-zA-Z ]");
                Match categoryMatch = regexName.Match(categoryDTO.Name);

                if (!categoryMatch.Success)
                {
                    var newErrorResponse = new ErrorResponseDTO
                    {
                        Error = "Internal Server Error",
                        Message = "La categoria solo acepta letras",
                        Status = 500
                    };

                    return Result<string>.Failure(newErrorResponse);
                } else
                {
                    category.Name = categoryDTO.Name;
                }
            }

            if (categoryDTO.ImageUrl != null)
            {
                category.ImageUrl = categoryDTO.ImageUrl;
            }

            if (categoryDTO.IsDisabled != null)
            {
                category.IsDisabled = (bool)categoryDTO.IsDisabled;
            }

            _categoryRepository.Save(category);

            return Result<string>.Success("Actualización exitosa");
        }
    }
}
