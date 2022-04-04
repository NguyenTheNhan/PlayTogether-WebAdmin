using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.Transaction;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpTransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;

        public HttpTransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TransactionSummary>> GetTransactionsAsync(string userId,
                                                                          string type = null,
                                                                          DateTime? fromDate = null,
                                                                          DateTime? toDate = null,
                                                                          int pageNumber = 1,
                                                                          int pageSize = 1000)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/admins/transactions/{userId}?FromDate={fromDate}&ToDate={toDate}&Type={type}&PageNumber={pageNumber}&PageSize={pageSize}&IsNew=true");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<TransactionSummary>>();
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
