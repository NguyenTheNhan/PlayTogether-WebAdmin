using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.TypeOfGame;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpTypeOfGameService : ITypeOfGameService
    {
        private readonly HttpClient _httpClient;

        public HttpTypeOfGameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<TypeOfGameDetail> CreateAsync(string gameId, string gameTypeId)
        {

            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/games", new
            {
                GameId=gameId,
                GameTypeId=gameTypeId,
            });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TypeOfGameDetail>();
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
            var response = await _httpClient.DeleteAsync($"/api/play-together/v1/types-of-game/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }


        public async Task<TypeOfGameDetail> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/types-of-game/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TypeOfGameDetail>();
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
