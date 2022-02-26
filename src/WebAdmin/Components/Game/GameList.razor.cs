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
using WebAdmin.Shared.Models.Game;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using AKSoftware.Blazor.Utilities;

namespace WebAdmin.Components
{
    public partial class GameList
    {
        [Inject]
        public IGameService GameService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;


        private List<GameSummary> _games = new();

        private async Task<IEnumerable<GameSummary>> GetGamesAsync(string query = "", int pageNumber = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await GameService.GetGamesAsync(query, pageNumber, pageSize);
                _games = result.ToList();

                return result;
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
            return null;
        }

        #region Edit
        private void EditGame(GameSummary game)
        {
            Navigation.NavigateTo($"/games/form/{game.Id}");
        }
        #endregion

        #region Delete
        private async Task DeleteGameAsync(GameSummary game)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete '{game.Name}'?");
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
                    await GameService.DeleteAsync(game.Id);

                    // Send a message about the deleted game type
                    MessagingCenter.Send(this, "game_deleted", game);
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