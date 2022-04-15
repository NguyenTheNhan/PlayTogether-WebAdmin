using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Report;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpReportService : IReportService
    {
        private readonly HttpClient _httpClient;

        public HttpReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ActiveAsync(string id, bool isApprove, int point = 0, int satisfiedPoint = 0, bool isDisableAccount = false)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/admins/reports/{id}", new
            {
                isApprove = isApprove,
                point = point,
                satisfiedPoint = satisfiedPoint,
                isDisableAccount = isDisableAccount,
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

        public async Task<ApiResponse<ReportDetails>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/admins/reports/{id}");


            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ReportDetails>>();
            if (result.IsSuccess)
            {
                return result;

            }
            else
            {
                var errorResponse = result.Error;
                throw new ApiException(errorResponse, response.StatusCode);
            }

        }

        public async Task<PagedList<ReportSummary>> GetByUserIdAsync(string userId, bool? isApprove = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/reports/{userId}?IsApprove={isApprove}&FromDate={fromDate}&ToDate={toDate}&PageNumber={pageNumber}&PageSize={pageSize}&IsNew=true");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PagedList<ReportSummary>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<PagedList<ReportSummary>> GetReportsAsync(bool? isApprove = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/admins/reports?IsApprove={isApprove}&FromDate={fromDate}&ToDate={toDate}&PageNumber={pageNumber}&PageSize={pageSize}&IsNew=true");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PagedList<ReportSummary>>();
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
