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
using AKSoftware.Blazor.Utilities;

namespace WebAdmin.Components
{
    public partial class GameTypeList
    {
        [Inject]
        public IGameTypeService GameTypeService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private List<GameTypeSummary> _gameTypes = new();

        private async Task<IEnumerable<GameTypeSummary>> GetGameTypesAsync(string query = "", int pageNumber = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await GameTypeService.GetGameTypesAsync(query, pageNumber, pageSize);
                _gameTypes = result.ToList();

                return result;
            }
            catch (ApiException ex)
            {
               // _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Error.HandleError(ex);
            }
            _isBusy = false;
            return null;
        }

        #region Edit
        private void EditGameType(GameTypeSummary gameType)
        {
            Navigation.NavigateTo($"/gametypes/form/{gameType.Id}");
        }
        #endregion

        #region Delete
        private async Task DeleteGameTypeAsync(GameTypeSummary gameType)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete '{gameType.Name}'?");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Delete", parameters, options);
            var confirmationResult = await dialog.Result;

            if (!confirmationResult.Cancelled)
            {
                // Confirmed to delete
                try
                {
                    await GameTypeService.DeleteAsync(gameType.Id);

                    // Send a message about the deleted game type
                    MessagingCenter.Send(this, "gameType_deleted", gameType);
                }
                catch (ApiException ex)
                {
                    // TODO: Log this error 
                }
                catch (Exception ex)
                {
                    // TODO: Log this error 
                }

            }
        }
        #endregion


    }
}