using System;

namespace WebAdmin.Shared.Models
{
    public class LoginResult
    {
        public string Message { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsSuccess { get; set; }
    }
}
