namespace get_sales_result.Application.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(T data, bool success = true, string? message = null)
        {
            Data = data;
            Success = success;
            Message = message;
        }

        private ApiResponse(string message, bool success)
        {
            Data = default;
            Message = message;
            Success = success;
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>(message, false);
        }
    }

}
