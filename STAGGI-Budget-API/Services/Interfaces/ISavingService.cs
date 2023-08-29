using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ISavingService
    {
        public Result<List<SavingDTO>> GetAll(string email);
        public Result<SavingDTO> GetSavingById(int id);
        public Result<string> CreateSaving(RequestSavingDTO savingDTO, string email);
        public Result<string> UpdateSaving(int id, RequestSavingDTO savingDTO, string email);
    }
}
