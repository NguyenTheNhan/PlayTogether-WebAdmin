using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Hirer;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpHirerService : IHirerService
    {
        private readonly HttpClient _httpClient;

        public HttpHirerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PagedList<HirerSummary>> GetHirersAsync(string query = null, string status = null, bool? isActive = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/admins/users?Name={query}&Status={status}&IsActive={isActive}&PageNumber={pageNumber}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PagedList<HirerSummary>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
        public async Task<UserDetail> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserDetail>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }


        public async Task<UserDetail> ActiveAsync(string id, bool isActive, int numDateDisable, DateTime? dateDisable)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/admins/users/activate/{id}", new
            {
                isActive = isActive,
                note = isActive == true ? "Khoá" : "Mở khoá",
                numDateDisable = numDateDisable,
                dateDisable = dateDisable,
                dateActive = dateDisable?.AddDays(numDateDisable),
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
