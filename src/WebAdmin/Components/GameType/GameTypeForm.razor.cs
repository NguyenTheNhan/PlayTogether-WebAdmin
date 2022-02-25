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
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using WebAdmin;
using WebAdmin.Shared;
using WebAdmin.Components;
using MudBlazor;
using Blazored.FluentValidation;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Shared.Models.GameType;

namespace WebAdmin.Components
{
    public partial class GameTypeForm
    {
        [Inject]
        public IGameTypeService GameTypeService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isEditMode => Id != null;

        private GameTypeSummary _model = new GameTypeSummary();
        private bool _isBusy = false;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (_isEditMode)
                await FetchGameTypeByIdAsync();
        }

        private async Task SubmitFormAsync()
        {
            _isBusy = true;
            try
            {
                if (_isEditMode)
                {
                    await GameTypeService.EditAsync(_model, Id);
                }
                else
                {
                    var result = await GameTypeService.CreateAsync(_model);
                }
                //success
                Navigation.NavigateTo("/gametypes");
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error.HandleError(ex);

            }

            _isBusy = false;

        }

        private async Task FetchGameTypeByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await GameTypeService.GetByIdAsync(Id);
                _model = result;

            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //TODO: log the error
                Error.HandleError(ex);

            }

            _isBusy = false;
        }
    }
}