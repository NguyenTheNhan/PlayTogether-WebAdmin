using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebAdmin
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {

        private readonly ILocalStorageService _storage;

        public AuthenticationStateProvider AuthenticationStateProvider;


        public AuthorizationMessageHandler(ILocalStorageService storage)
        {
            _storage = storage;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (await _storage.ContainKeyAsync("access_token"))
            {
                var now = DateTime.Now;
                var time = await _storage.GetItemAsync<DateTime>("expire_date");
                var compare = DateTime.Compare(time, now);
                if (compare > 0)
                {
                    var token = await _storage.GetItemAsStringAsync("access_token");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                else
                {

                    await _storage.RemoveItemAsync("access_token");
                    await _storage.RemoveItemAsync("expire_date");

                    await AuthenticationStateProvider.GetAuthenticationStateAsync();


                }
            }
            Console.WriteLine("Authorization Message Handler Called");
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
