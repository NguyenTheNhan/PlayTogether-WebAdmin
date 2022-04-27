using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Game;

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

        private async Task<PagedList<GameSummary>> GetGamesAsync(string query = "", int pageNumber = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await GameService.GetGamesAsync(query, pageNumber, pageSize);
                _games = result.Content.ToList();

                return result;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
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
            parameters.Add("ContentText", $"Bạn có muốn xoá '{game.Name}'?");
            parameters.Add("ButtonText", "Xoá");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Xoá game", parameters, options);
            var confirmationResult = await dialog.Result;

            if (!confirmationResult.Cancelled)
            {
                // Confirmed to delete
                try
                {
                    await GameService.DeleteAsync(game.Id);

                    Error.HandleSuccess("Xoá game thành công");
                    // Send a message about the deleted game type
                    MessagingCenter.Send(this, "game_deleted", game);
                }
                catch (ApiException ex)
                {
                    // TODO: Log this error 
                    _errorMessage = ex.ApiErrorResponse.Message ?? ex.Message;
                    Error.HandleError(_errorMessage);
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