using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
        public async Task<GameTypeSummary> CreateAsync(GameTypeSummary model)
        {            

            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/game-types", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GameTypeSummary>();
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

        public async Task<GameTypeSummary> EditAsync(GameTypeSummary model, string id)
        {         

            var response = await _httpClient.PutAsJsonAsync<object>($"/api/play-together/v1/game-types/{id}", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GameTypeSummary>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<GameTypeSummary> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/game-types/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GameTypeSummary>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<IEnumerable<GameTypeSummary>> GetGameTypesAsync(string query = null, int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/game-types?SearchString={query}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<GameTypeSummary>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        private HttpContent PrepareGameTypeForm(GameTypeSummary model, bool isUpdate)
        {
            var form = new MultipartFormDataContent();

            if (!string.IsNullOrWhiteSpace(model.Name))
                form.Add(new StringContent(model.Name), nameof(GameTypeSummary.Name));
            if (!string.IsNullOrWhiteSpace(model.Description))
                form.Add(new StringContent(model.Description), nameof(GameTypeSummary.Description));
            if (!string.IsNullOrWhiteSpace(model.ShortName))
                form.Add(new StringContent(model.ShortName), nameof(GameTypeSummary.ShortName));
            if (!string.IsNullOrWhiteSpace(model.OtherName))
                form.Add(new StringContent(model.OtherName), nameof(GameTypeSummary.OtherName));
            if (isUpdate)
                form.Add(new StringContent(model.Id), nameof(GameTypeSummary.Id));


            return form;
        }
    }
}
