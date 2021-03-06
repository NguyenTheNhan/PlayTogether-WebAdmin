using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Report;

namespace WebAdmin.Components
{
    public partial class ReportList
    {
        [Inject]
        public IReportService ReportService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        // private List<ReportSummary> _reports = new();



        #region View
        private void ViewReport(ReportSummary report)
        {
            //Navigation.NavigateTo($"/hirers/order/{order.Id}");

            var parameters = new DialogParameters();
            parameters.Add("Id", report.Id);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<ReportDetailDialog>("Th?ng tin report", parameters, options);
        }
        #endregion

    }
}