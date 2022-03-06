using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.Player;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpPlayerService : IPlayerService
    {
        private readonly HttpClient _httpClient;

        public HttpPlayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<PlayerSummary>> GetPlayersAsync(string query = null, string status = null, bool? isActive = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/players?Name={query}&Status={status}&IsActive={isActive}&PageNumber={pageNumber}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<PlayerSummary>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
        public async Task<PlayerSummary> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/players/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PlayerSummary>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }


        public async Task<PlayerSummary> ActiveAsync(string id, bool isActive, string message)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/admins/player-status/{id}", new
            {
                isActive = isActive,
                message = message,
            });
            if (response.IsSuccessStatusCode)
            {
                var result = await GetByIdAsync(id);
                return result;
            }
            else
            {

                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
    }
}
