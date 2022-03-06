using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Rank;

namespace WebAdmin.Components
{
    public partial class RankDetailDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IRankService RankService { get; set; }

        [Parameter]
        public string RankId { get; set; }
        [Parameter]
        public string GameId { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }

        private RankDetail _rank;
        private bool _isBusy;
        private string _errorMessage = string.Empty;

        private void Close()
        {
            MudDialog.Cancel();
        }

        protected override void OnParametersSet()
        {
            if (RankId == null)
                throw new ArgumentNullException(nameof(RankId));

            base.OnParametersSet();
        }

        protected async override Task OnInitializedAsync()
        {
            await FetchRankAsync();
        }
        private async Task FetchRankAsync()
        {
            _isBusy = true;
            try
            {
                var result = await RankService.GetByIdAsync(RankId);
                _rank = result;
                StateHasChanged();
            }
            catch (ApiException ex)
            {
                //Log this error
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //Log this error
                Error.HandleError(ex);
            }

            _isBusy = false;
        }

        private void OnItemEditedCallBack(RankDetail rank)
        {
            var editedItem = rank;
            editedItem.Name = rank.Name;
            editedItem.No = rank.No;

        }

    }
}