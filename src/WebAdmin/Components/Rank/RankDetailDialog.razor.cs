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
using WebAdmin.Client.Services.Exceptions;

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



    }
}