using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.Order;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpOrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public HttpOrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<OrderDetail> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrdersAsync(string userId, string status = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/admins/{userId}/orders?FromDate={fromDate}&ToDate={toDate}&Status={status}&PageNumber={pageNumber}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDetail>>();
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
