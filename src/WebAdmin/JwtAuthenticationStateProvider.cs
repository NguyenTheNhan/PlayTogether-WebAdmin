using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAdmin
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;
        public JwtAuthenticationStateProvider(ILocalStorageService storage)
        {
            _storage = storage;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storage.ContainKeyAsync("access_token"))
            {
                //var now = DateTime.Now;
                //var time = await _storage.GetItemAsStringAsync("expire_date");
                //DateTime storeDate = DateTime.Parse(time);
                //var compare = DateTime.Compare(storeDate, now);
                //if (compare > 0)
                //{

                //the user is logged in
                var tokenAsString = await _storage.GetItemAsStringAsync("access_token");
                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.ReadJwtToken(tokenAsString);
                var identity = new ClaimsIdentity(token.Claims, "Bearer");
                var user = new ClaimsPrincipal(identity);

                var authState = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(authState));

                return authState;
                //}
                //else
                //{
                //    await _storage.RemoveItemAsync("access_token");
                //    await _storage.RemoveItemAsync("expire_date");

                //    return new AuthenticationState(new ClaimsPrincipal());

                //}
            }

            //Empty claim principal mean no identity and user is not logged in
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}
