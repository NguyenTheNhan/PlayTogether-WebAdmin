using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Charities;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpCharitiesService : ICharitiesService
    {
        private readonly HttpClient _httpClient;

        public HttpCharitiesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateAsync(CharityCreate model)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/accounts/register-charity", model);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("đăng ký ok nè");

            }
            else
            {
                Console.WriteLine("đăng ký không Ok rồi");
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }


        public async Task EditAsync(CharitiesSummary model)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/charities/{model.Id}", model);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<CharitiesSummary>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/charities/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<CharitiesSummary>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<PagedList<CharitiesSummary>> GetCharitiesAsync(string query = null, bool isActive = true, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/charities?Name={query}&IsActive={isActive}&PageNumber={pageNumber}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PagedList<CharitiesSummary>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
        public async Task ActiveAsync(string id, bool isActive)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/admins/charities/{id}", new
            {
                isActive = isActive,
            });
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }


    }
}
