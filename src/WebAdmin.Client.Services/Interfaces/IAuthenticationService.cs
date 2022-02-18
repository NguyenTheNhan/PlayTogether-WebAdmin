using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {

        Task<LoginResult> LoginUserAsync(LoginRequest model);

    }
}
