using System;

namespace WebAdmin.Shared.Responses
{

    public class ApiResponse
    {
        public ApiErrorResponse Error { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime ResponseTime { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Content { get; set; }


    }
}