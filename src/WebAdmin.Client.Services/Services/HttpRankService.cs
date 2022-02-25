﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Rank;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Services
{
    public class HttpRankService :IRankService
    {
        private readonly HttpClient _httpClient;

        public HttpRankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RankDetail> CreateAsync(RankDetail model, string gameId)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/play-together/v1/games/{gameId}/ranks", new
            {
                GameId = gameId,
                No = model.No,
                Name = model.Name,
            });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RankDetail>();
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
            var response = await _httpClient.DeleteAsync($"/api/play-together/v1/ranks/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<RankDetail> EditAsync(string id, int no, string name, string gameId)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/play-together/v1/ranks/{id}", new
            {
                GameId=gameId,
                No = no,
                Name = name,
                Id=id
            });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RankDetail>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<RankDetail> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/ranks/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RankDetail>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<PagedList<RankDetail>> GetRankAsync(string gameId)
        {
            var response = await _httpClient.GetAsync($"/api/play-together/v1/games/{gameId}/ranks");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PagedList<RankDetail>> ();
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
