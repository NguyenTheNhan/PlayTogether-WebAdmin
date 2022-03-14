using System;

namespace WebAdmin.Shared.Models.Auth
{
    public class LoginResult
    {
        public string Message { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}
