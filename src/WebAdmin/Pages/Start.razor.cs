using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;
using WebAdmin.Shared;

namespace WebAdmin.Pages
{
    public partial class Start
    {
        [CascadingParameter]
        public Error Error { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LogoutAsync();
        }

        protected async Task LogoutAsync()
        {
            if (await Storage.ContainKeyAsync("access_token"))
            {
                var now = DateTime.Now;
                var time = await Storage.GetItemAsync<DateTime>("expire_date");
                var compare = DateTime.Compare(time, now);
                if (compare > 0)
                {
                    NavManager.NavigateTo("/index");
                }
                else
                {
                    await Storage.RemoveItemAsync("access_token");
                    await Storage.RemoveItemAsync("expire_date");

                    Error.HandleError("Phiên đăng nhập hết hạn");
                    await AuthenticationStateProvider.GetAuthenticationStateAsync();
                    NavManager.NavigateTo("/authentication/login");
                }

            }
            else
            {
                NavManager.NavigateTo("/authentication/login");
            }
        }
    }
}