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
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.TypeOfGame;
using WebAdmin.Shared.Models.GameType;

namespace WebAdmin.Components
{
    public partial class CreateTypeOfGameItem
    {
        [Inject]
        public ITypeOfGameService TypeOfGameService { get; set; }
        [Parameter]
        public string GameId { get; set; }
        [Parameter]
        public List<GameTypeSummary> GameTypes { get; set; }

        [Parameter]
        public EventCallback<TypeOfGameWithGameTypeDetail> OnTypeOfGameAdded { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private GameTypeSummary model { get; set; }

        private string _errorMessage = string.Empty;
        

        private async Task AddTypeOfGameItemAsync()
        {
            _errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(model.Id))
                {
                    _errorMessage = "GameType is required";
                    return;
                }
                _isBusy = true;
                //Call Api to add TypeOfGame Item
                var result = await TypeOfGameService.CreateAsync( GameId, model.Id);
                

                //Notify the parent about the newly added item
                await OnTypeOfGameAdded.InvokeAsync();
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {

                //TypeOfGame: Handle error globally
                Error.HandleError(ex);
            }
            _isBusy = false;
        }
    }
}