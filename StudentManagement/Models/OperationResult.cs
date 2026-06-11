namespace StudentManagement.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic? Data { get; set; }

        public static OperationResult Success(string message, dynamic? data = null)
            => new() { IsSuccess = true, Message = message, Data = data };

        public static OperationResult Fail(string message)
            => new() { IsSuccess = false, Message = message };
    }
}