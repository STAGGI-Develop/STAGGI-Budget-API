using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Helpers;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ITransactionService
    {
        public Result<List<TransactionDTO>> GetAll();
        //public Result<TransactionDTO> GetById(long id);
        public Result<TransactionDTO> CreateTransaction(TransactionDTO transactionDTO);
        public Result<TransactionDTO> ModifyTransaction(long transactionId, TransactionDTO transactionDTO);
    }
}
