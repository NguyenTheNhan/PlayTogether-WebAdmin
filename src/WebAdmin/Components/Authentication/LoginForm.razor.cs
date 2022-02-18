using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using WebAdmin;
using WebAdmin.Shared;
using MudBlazor;
using WebAdmin.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using WebAdmin.Shared.Models;
using WebAdmin.Client.Services.Exceptions;

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

                    Navigation.NavigateTo("/");
                }

            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {

                Error.HandleError(ex);
            }

            _isBusy = false;
        }

        

    }
}