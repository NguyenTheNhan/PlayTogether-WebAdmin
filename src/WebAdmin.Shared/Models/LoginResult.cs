using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Shared.Models
{
    public class LoginResult
    {
        public string Message { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsSuccess { get; set; }
    }
}
