using PTO_WebAdmin.Client.Services.Interfaces;
using PTO_WebAdmin.Shared.Models;
using PTO_WebAdmin.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PTO_WebAdmin.Client.Services
{
    public class HttpAuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private string _errorMessage = string.Empty;
        public HttpAuthenticationService(HttpClient client)
        {
            _client = client;
        }

        
        public async Task<LoginResult> LoginUserAsync(LoginRequest model)
        {
            _errorMessage = string.Empty;

            var response = await _client.PostAsJsonAsync("/api/v2/auth/login", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                _errorMessage = errorResult.Errors.SingleOrDefault();
                return null;
            }

        }
    }
}

