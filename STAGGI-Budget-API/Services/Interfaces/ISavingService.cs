using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ISavingService
    {
        public Result<List<SavingDTO>> GetAll();
        public Result<SavingDTO> GetSavingById(long id);
        public Result<SavingDTO> CreateSaving(SavingDTO savingDTO);
        public Result<SavingDTO> ModifySaving(long savingId, SavingDTO savingDTO);
        public Result<List<SavingDTO>> SearchSaving(string searchParameter);
    }
}
