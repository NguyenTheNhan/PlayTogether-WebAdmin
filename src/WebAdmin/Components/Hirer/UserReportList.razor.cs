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
using WebAdmin.Shared.Models.Report;

namespace WebAdmin.Components
{
    public partial class UserReportList
    {
        [Inject]
        public IReportService ReportService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private List<ReportSummary> _reports = new();

        protected override async Task OnInitializedAsync()
        {
            _isBusy = true;
            await GetReportAsync();
            _isBusy = false;
        }
        private async Task<PagedList<ReportSummary>> GetReportAsync()
        {
            _isBusy = true;
            try
            {
                var result = await ReportService.GetByUserIdAsync(UserId);
                _reports = result.Content.ToList();
                return result;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
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
        private void ViewReport(ReportSummary report)
        {
            //Navigation.NavigateTo($"/hirers/order/{order.Id}");

            var parameters = new DialogParameters();
            parameters.Add("Id", report.Id);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<ReportDetailDialog>("Thông tin report", parameters, options);
        }
        #endregion
    }
}