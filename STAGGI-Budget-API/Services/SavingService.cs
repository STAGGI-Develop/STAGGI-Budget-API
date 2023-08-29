using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services.Interfaces;
using System.Text.RegularExpressions;

namespace STAGGI_Budget_API.Services
{
        public class SavingService : ISavingService
        {
            private readonly ISavingRepository _savingRepository;

            public SavingService(ISavingRepository savingRepository)
            {
                _savingRepository = savingRepository;
            }

            public Result<List<SavingDTO>> GetAll()
            {
                var result = _savingRepository.GetAll();
                var savingsDTO = new List<SavingDTO>();

                foreach (var saving in result)
                {
                    savingsDTO.Add(new SavingDTO
                    {
                        Name = saving.Name,
                        Balance = saving.Balance,
                        TargetAmount = saving.TargetAmount,
                        DueDate = DateTime.Now,
                        //CategoryId = saving.CategoryId,
                    });
                }

                return Result<List<SavingDTO>>.Success(savingsDTO);
            }

            public Result<SavingDTO> CreateSaving(SavingDTO savingDTO)
            {
                try
                {
                    _savingRepository.Save(new Saving
                    {
                        Name = savingDTO.Name,
                        Balance = savingDTO.Balance,
                        TargetAmount = savingDTO.TargetAmount,
                        DueDate = DateTime.Now,
                        //CategoryId = savingDTO.CategoryId,
                    });

                    return Result<SavingDTO>.Success(savingDTO);
                }
                catch
                {
                    return Result<SavingDTO>.Failure(new ErrorResponseDTO
                    {
                        Status = 500,
                        Error = "Internal Server Error",
                        Message = "No se pudo realizar la meta."
                    });
                }
            }
            public Result<SavingDTO> ModifySaving(long savingId, SavingDTO savingDTO)
            {
                try
                {
                    var saving = _savingRepository.FindById(savingId) ?? throw new KeyNotFoundException($"Article with id: {savingId} not found");

                    saving.Name = savingDTO.Name;
                    saving.Balance = savingDTO.Balance;
                    saving.TargetAmount = savingDTO.TargetAmount;
                    //saving.CategoryId = savingDTO.CategoryId;

                    return Result<SavingDTO>.Success(savingDTO);

                }
                catch
                {
                    return Result<SavingDTO>.Failure(new ErrorResponseDTO
                    {
                        Status = 500,
                        Error = "Internal Server Error",
                        Message = "No se pudo actualizar la meta."
                    });
                }
            }

            public Result<List<SavingDTO>> SearchSaving(string searchParameter)
            {
                Regex regexName = new Regex("[A-Z0-9]");

                if (searchParameter == null)
                {
                    var newErrorResponse = new ErrorResponseDTO
                    {
                        Error = "Server Error",
                        Message = "Usted no ingreso ningun dato a buscar.",
                        Status = 500
                    };

                    return Result<List<SavingDTO>>.Failure(newErrorResponse);
                }

                if (searchParameter.Length > 15)
                {
                    var newErrorResponse = new ErrorResponseDTO
                    {
                        Error = "Server Error",
                        Message = "La longitud de la busqueda supera el maximo de caracteres.",
                        Status = 500
                    };

                    return Result<List<SavingDTO>>.Failure(newErrorResponse);
                }

                Match searchMatch = regexName.Match(searchParameter);
                if (!searchMatch.Success)
                {
                    var newErrorResponse = new ErrorResponseDTO
                    {
                        Error = "Server Error",
                        Message = "Usted no puede utilizar caracteres especiales para buscar.",
                        Status = 500
                    };

                    return Result<List<SavingDTO>>.Failure(newErrorResponse);
                }

                var savingSearch = _savingRepository.Search(searchParameter);
                var savingSearchDTO = new List<SavingDTO>();
                foreach (Saving saving in savingSearch)
                {
                    SavingDTO newSavingSearchDTO = new SavingDTO
                    {
                        Name = saving.Name,
                        Balance = saving.Balance,
                        TargetAmount = saving.TargetAmount,
                        DueDate = saving.DueDate,
                    };

                    savingSearchDTO.Add(newSavingSearchDTO);
                }

                if (savingSearchDTO == null)
                {
                    return Result<List<SavingDTO>>.Failure(new ErrorResponseDTO
                    {
                        Status = 204,
                        Error = "Error en la busqueda",
                        Message = "No se pudo encontrar la transaccion buscada."
                    });
                }

                return Result<List<SavingDTO>>.Success(savingSearchDTO);
            }

            public Result<SavingDTO> GetSavingById(long id)
            {
                var saving = _savingRepository.FindById(id);

                var savingDTO = new SavingDTO
                {
                    Name = saving.Name,
                    Balance = saving.Balance,
                    TargetAmount = saving.TargetAmount,
                    DueDate = saving.DueDate
                };

                return Result<SavingDTO>.Success(savingDTO);

            }
    }
}
