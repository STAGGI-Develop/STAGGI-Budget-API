namespace STAGGI_Budget_API.DTOs
{
    public class ErrorResponseDTO
    {
        //public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
