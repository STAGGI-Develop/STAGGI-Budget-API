using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.DTOs.Request;
using STAGGI_Budget_API.Enums;
using STAGGI_Budget_API.Helpers;
using STAGGI_Budget_API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace STAGGI_Budget_API.Services.Interfaces
{
    public interface ITransactionService
    {
        Result<List<TransactionDTO>> GetAllByUserEmail(string userEmail);
        Result<TransactionDTO> GetTransactionById(int id);
        Result<string> CreateTransaction(RequestTransactionDTO request, string email);
        Result<TransactionDTO> ModifyTransaction(int transactionId, RequestTransactionDTO request);
        Result<List<TransactionDTO>> SearchTransactionByKeyword(string searchParameter, string email);
        Result<string> DeleteTransactionById(int id);
        Result<List<TransactionDTO>> SearchTransactionByDate(DateTime? fromDate, DateTime? toDate, string email);
        Result<List<TransactionDTO>> SearchTransactionByType(TransactionType type, string email);
        Result<List<TransactionDTO>> SearchTransactionByKeywordAndType(string keyword, TransactionType type, string email);
        Result<List<TransactionDTO>> SearchTransactionByDateAndType(DateTime? fromDate, DateTime? toDate, TransactionType type, string email);
        Result<List<TransactionDTO>> SearchTransactionByKeywordAndDate(string keyword, DateTime? fromDate, DateTime? toDate, string email);
        Result<List<TransactionDTO>> SearchTransactionByAllFilters(string keyword, DateTime? fromDate, DateTime? toDate, TransactionType type, string userEmail);
        Result<List<TransactionDTO>> SearchLastTransactionsByEmail(string email);
    }
}
