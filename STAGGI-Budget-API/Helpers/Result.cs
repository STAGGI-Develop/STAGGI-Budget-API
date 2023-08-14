using STAGGI_Budget_API.DTOs;
using System;

namespace STAGGI_Budget_API.Helpers
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Ok { get; }
        public ErrorResponseDTO Error { get; }

        private Result(bool isSuccess, T result, ErrorResponseDTO error)
        {
            IsSuccess = isSuccess;
            Ok = result;
            Error = error;
        }

        public static Result<T> Success(T result)
        {
            return new Result<T>(true, result, null);
        }

        public static Result<T> Failure(ErrorResponseDTO error)
        {
            return new Result<T>(false, default(T), error);
        }

        public static Result<T> Unauthorized()
        {
            var unauthorizedResponse = new ErrorResponseDTO
            {
                Status = 401,
                Error = "Unauthorized",
                Message = "Usuario no autenticado"
            };

            return new Result<T>(false, default(T), unauthorizedResponse);
        }
    }
}
