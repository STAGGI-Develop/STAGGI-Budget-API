using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ITransactionService
    {
        public Result<List<TransactionDTO>> GetAllByUserEmail(string userEmail);
        public Result<TransactionDTO> GetTransactionById(long id);
        public Result<TransactionDTO> CreateTransaction(CreateTransactionDTO transactionDTO);
        public Result<TransactionDTO> ModifyTransaction(long transactionId, TransactionDTO transactionDTO);
        public Result<List<TransactionDTO>> SearchTransaction(string searchParameter, string userEmail);
    }
}
