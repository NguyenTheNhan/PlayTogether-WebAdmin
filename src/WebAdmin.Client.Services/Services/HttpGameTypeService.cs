using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.GameType;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpGameTypeService : IGameTypeService
    {
        private readonly HttpClient _httpClient;

        public HttpGameTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<GameTypeSummary>> CreateAsync(GameTypeSummary model)
        {

            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/game-types", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<GameTypeSummary>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"/api/play-together/v1/game-types/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<GameTypeSummary>> EditAsync(GameTypeSummary model)
        {

            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/game-types/{model.Id}", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await GetByIdAsync(model.Id);
                return result;
            }
            else
            {

                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<GameTypeSummary>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/game-types/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<GameTypeSummary>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<PagedList<GameTypeSummary>> GetGameTypesAsync(string query = null, int pageNumber = 1, int pageSize = 1000)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/game-types?Name={query}&PageNumber={pageNumber}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PagedList<GameTypeSummary>>();
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
