﻿using PTO_WebAdmin.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTO_WebAdmin.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {
        
        Task<LoginResult> LoginUserAsync(LoginRequest model);

    }
}
