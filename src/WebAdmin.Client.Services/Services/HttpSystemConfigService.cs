using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.SystemConfig;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpSystemConfigService : ISystemConfigService
    {
        private readonly HttpClient _httpClient;

        public HttpSystemConfigService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<SystemConfigDetail>> CreateAsync(SystemConfigDetail model)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/system-configs", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<SystemConfigDetail>>();
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
            var response = await _httpClient.DeleteAsync($"/api/play-together/v1/system-configs/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task EditAsync(string id, float value)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/system-configs/{id}", new
            {
                value = value,
            });
            if (!response.IsSuccessStatusCode)

            {

                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<SystemConfigDetail>> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/system-configs/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<SystemConfigDetail>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<SystemConfigDetail>> GetByNoAsync(string no)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/system-configs/no-{no}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<SystemConfigDetail>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }


        public async Task<PagedList<SystemConfigDetail>> GetSystemConfigAsync(string query = null, int pageNumber = 1, int pageSize = 1000)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/system-configs?Title={query}&PageNumber={pageNumber}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PagedList<SystemConfigDetail>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task MultiUpdate(List<SystemConfigDetail> systemConfigs)
        {
            foreach (var item in systemConfigs)
            {
                await EditAsync(item.Id, item.Value);
            }
        }

        public async Task MaintainSystem()
        {
            var response = await _httpClient.PutAsync($"/api/play-together/v1/admins/maintain", null);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
        public async Task<ApiResponse<TrainModelResponse>> TrainModelRecommendation()
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/admins/train-model");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<TrainModelResponse>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task NotifyAll(string title, string message, string referenceLink = "")
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/notification/all", new
            {
                title = title,
                message = message,
                referenceLink = referenceLink,
            });
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
        public async Task SendNotification(string receiverId, string title, string message, string referenceLink = "")
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/notification", new
            {
                receiverId = receiverId,
                title = title,
                message = message,
                referenceLink = referenceLink,
            });
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
        public async Task SendEmail(string toEmail, string subject, string body)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/email/send", new
            {
                toEmail = toEmail,
                subject = subject,
                body = body
            });
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
    }
}
