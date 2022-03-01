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
using WebAdmin.Shared.Models.TypeOfGame;
using WebAdmin.Client.Services.Exceptions;

namespace WebAdmin.Components
{
    public partial class TypeOfGameItem
    {
        [Inject]
        public ITypeOfGameService TypeOfGameService { get; set; }

        [Parameter]
        public TypeOfGameWithGameTypeDetail Item { get; set; }
        [Parameter]
        public string GameId { get; set; }
        [Parameter]
        public EventCallback<TypeOfGameWithGameTypeDetail> OnItemDeleted { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        public bool _isBusy = false;

        private string _errorMessage = string.Empty;

        private async Task RemoveItemAsync()
        {
            try
            {
                _isBusy = true;
                //Call Api to add ToDo Item
                await TypeOfGameService.DeleteAsync(Item.Id);


                //Notify the parent about the newly added item
                await OnItemDeleted.InvokeAsync(Item);
            }
            catch (ApiException ex)
            {
                //TODO: Handle error globally
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {

                //TODO: Handle error globally
                Error.HandleError(ex);
            }
            _isBusy = false;
        }

    }
}