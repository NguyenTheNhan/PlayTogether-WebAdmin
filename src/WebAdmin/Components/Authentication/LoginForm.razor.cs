using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Auth;

namespace WebAdmin.Components
{
    public partial class LoginForm : ComponentBase
    {

        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ILocalStorageService Storage { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }

        private LoginRequest _model = new LoginRequest();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;


        private async Task LoginUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                var result = await AuthenticationService.LoginUserAsync(_model);
                if (result != null)
                {

                    // Store it in local storage 
                    await Storage.SetItemAsStringAsync("access_token", result.Message);
                    await Storage.SetItemAsync<DateTime>("expire_date", result.ExpireDate);

                    await AuthenticationStateProvider.GetAuthenticationStateAsync();

                    Navigation.NavigateTo("/index");
                }

            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {

                _errorMessage = ex.Message;
            }

            _isBusy = false;
        }



    }
}