using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ITransactionService
    {
        Result<List<TransactionDTO>> GetAllByUserEmail(string userEmail);
        Result<TransactionDTO> GetTransactionById(int id);
        Result<string> CreateTransaction(RequestTransactionDTO request, string email);
        Result<TransactionDTO> ModifyTransaction(int transactionId, RequestTransactionDTO request);
        Result<List<TransactionDTO>> SearchTransaction(string searchParameter, string email);
        Result<string> DeleteTransactionById(int id);
    }
}
