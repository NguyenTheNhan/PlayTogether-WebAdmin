using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace WebAdmin.Shared
{
    public partial class MainLayout
    {
        bool _drawerOpen = true;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        MudTheme _lightTheme = new MudTheme { Palette = new Palette { } };
        [CascadingParameter]
        public Error Error { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LogoutAsync();
        }
        protected override async void OnInitialized()
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
                if (compare < 0)
                {
                    Error.HandleError("Phiên đăng nhập hết hạn");
                    await Storage.RemoveItemAsync("access_token");
                    await Storage.RemoveItemAsync("expire_date");

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