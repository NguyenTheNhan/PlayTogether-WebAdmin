using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Rating;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpRatingService : IRatingService
    {
        private readonly HttpClient _httpClient;
        public HttpRatingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ActiveAsync(string id, bool isApprove)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/rating/process/{id}", new
            {
                isApprove = isApprove
            });

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("approve ok nè");
            }
            else
            {

                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<RatingDetail>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/rating/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<RatingDetail>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<PagedList<RatingDetail>> GetRatingsAsync(bool? isActive = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/rating/violates?IsActive={isActive}&PageNumber={pageNumber}&PageSize={pageSize}&IsNew=true");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PagedList<RatingDetail>>();
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
