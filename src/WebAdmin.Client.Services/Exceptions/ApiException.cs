using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Exceptions
{
    public class ApiException : Exception
    {
        public ApiErrorResponse ApiErrorResponse { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(ApiErrorResponse error, HttpStatusCode statusCode) : this(error)
        {
            ApiErrorResponse = error;
            StatusCode = statusCode;
        }

        public ApiException(ApiErrorResponse error)
        {
            ApiErrorResponse = error;
        }
    }
}
