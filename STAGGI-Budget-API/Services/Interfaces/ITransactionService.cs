using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ITransactionService
    {
        public Result<List<TransactionDTO>> GetAllByUserEmail(string userEmail);
        public Result<TransactionDTO> GetTransactionById(int id);
        public Result<string> CreateTransaction(RequestTransactionDTO transactionDTO, string email);
        public Result<string> ModifyTransaction(int transactionId, RequestTransactionDTO transactionDTO);
        public Result<List<TransactionDTO>> SearchTransaction(string searchParameter, string email);
    }
}
