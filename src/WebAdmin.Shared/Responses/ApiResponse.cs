using System;

namespace WebAdmin.Shared.Responses
{

    public class ApiResponse
    {
        public object Error { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime ResponseTime { get; set; }
    }

    public class ApiResponse<T>
    {
        public T Content { get; set; }


    }
}