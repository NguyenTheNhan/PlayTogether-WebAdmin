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
using WebAdmin.Shared.Models.Rank;
using WebAdmin.Shared.Models;
using WebAdmin.Client.Services.Exceptions;
using AKSoftware.Blazor.Utilities;

namespace WebAdmin.Components
{
    public partial class RankList
    {
        [Inject]
        public IRankService RankService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public string GameId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
       
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private List<RankDetail> _ranks = new();


        private async Task<IEnumerable<RankDetail>> GetRankAsync()
        {
            _isBusy = true;
            try
            {
                var result = await RankService.GetRankAsync(GameId);
                _ranks = result.ToList();
                return result;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //TODO: log this error
                Error.HandleError(ex);
            }
            _isBusy = false;
            return null;
        }


        #region View
        private void ViewRank(RankDetail rank)
        {
            var parameters = new DialogParameters();
            parameters.Add("RankId", rank.Id);
            parameters.Add("GameId", GameId);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<RankDetailDialog>("Details", parameters, options);

        }
        #endregion

        #region Delete
        private async Task DeleteRankAsync(RankDetail rank)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Do you really want to delete '{rank.Name}'?");
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
                    await RankService.DeleteAsync(rank.Id);

                    // Send a message about the deleted rank type
                    MessagingCenter.Send(this, "rank_deleted", rank);
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