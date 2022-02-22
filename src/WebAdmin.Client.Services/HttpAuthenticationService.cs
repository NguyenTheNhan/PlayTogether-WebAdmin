using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services
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
            var response = await _client.PostAsJsonAsync("/api/play-together/v1/accounts/login", model);
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
