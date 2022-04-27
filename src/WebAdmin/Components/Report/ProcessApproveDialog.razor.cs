using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;

namespace WebAdmin.Components
{
    public partial class ProcessApproveDialog
    {
        [Inject]
        public IReportService ReportService { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }


        //  private ReportDetails _model = new();

        private bool _approve { get; set; } = true;

        private bool _isBusy { get; set; } = false;
        private string _errorMessage = string.Empty;
        private float _rating { get; set; } = 0;
        private int _point { get; set; } = 0;
        private int _satisfiedPoint { get; set; } = 0;
        private bool _isDisable { get; set; } = false;

        //protected override async Task OnInitializedAsync()
        //{
        //    //await FetchReportByIdAsync();
        //}

        private void Close()
        {
            MudDialog.Cancel();
        }
        private async Task ApproveAsync()
        {
            _isBusy = true;
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Xác nhận duyệt báo cáo?");
            parameters.Add("ButtonText", "Duyệt");
            parameters.Add("Color", Color.Primary);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Xác nhận", parameters, options);

            var confirmationResult = await dialog.Result;
            if (!confirmationResult.Cancelled)
            {
                try
                {
                    await ReportService.ActiveAsync(Id, _approve, _point, _satisfiedPoint, _isDisable);

                    //success
                    Error.HandleSuccess("Thao tác thành công");
                    //send a message about the approved
                    Navigation.NavigateTo($"/account-reports");
                    MessagingCenter.Send(this, "report_approved", Id);

                }
                catch (ApiException ex)
                {
                    _errorMessage = ex.ApiErrorResponse.Message;
                    Error.HandleError(_errorMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Error.HandleError(ex);

                }
            }
            _isBusy = false;

        }

    }
}