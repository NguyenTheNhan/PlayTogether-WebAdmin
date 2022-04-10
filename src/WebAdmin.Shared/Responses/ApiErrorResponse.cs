

namespace WebAdmin.Shared.Responses
{
    //public class ApiErrorResponse
    //{
    //    public string[] Errors { get; set; }
    //    public bool Success { get; set; }
    //}


    public class ApiErrorResponse
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }



}
