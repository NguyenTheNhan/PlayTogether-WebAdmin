using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Hirer;
using WebAdmin.Shared.Models.Report;

namespace WebAdmin.Components
{
    public partial class ReportDetailDialog
    {
        [Inject]
        public IReportService ReportService { get; set; }
        [Inject]
        public IHirerService HirerService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }


        private ReportDetails _model = new();
        private UserDetail _reporter = new();
        private UserDetail _reported = new();
        private bool _approve { get; set; } = true;
        private string _action = "Duyệt";
        private bool _isBusy { get; set; } = false;
        private string _errorMessage = string.Empty;
        private float _rating { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            await FetchReportByIdAsync();
        }

        private void Close()
        {
            MudDialog.Cancel();
        }

        private async Task FetchReportByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await ReportService.GetByIdAsync(Id);

                _model = result;
                var reporter = await HirerService.GetByIdAsync(_model.UserId);
                var reported = await HirerService.GetByIdAsync(_model.ToUserId);

                _reporter = reporter.Content;
                _reported = reported.Content;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                //TODO: log the error
                Error.HandleError(ex);

            }

            _isBusy = false;
        }

        private async void selectOption(bool option)
        {
            _approve = option;
            if (_approve)
            {
                _action = "Duyệt";
            }
            else
            {
                _action = "Không duyệt";
            }
        }

        private async Task ApproveAsync()
        {
            _isBusy = true;
            var parameters = new DialogParameters();
            parameters.Add("ContentText", _model.IsApprove == null ? (_approve ? "Bạn muốn duyệt báo cáo này?" : "Bạn không duyệt báo cáo này ?")
                                                                   : (_model.IsApprove == true ? "Bạn không duyệt báo cáo này ?" : "Bạn muốn duyệt báo cáo này?"));
            parameters.Add("ButtonText", _model.IsApprove == null ? (_approve ? "Duyệt" : "Không duyệt")
                                                                  : (_model.IsApprove == true ? "Không duyệt" : "Duyệt"));
            parameters.Add("Color", Color.Primary);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Xác nhận", parameters, options);

            var confirmationResult = await dialog.Result;
            if (!confirmationResult.Cancelled)
            {
                try
                {
                    await ReportService.ActiveAsync(_model.Id, _approve);

                    //success
                    Error.HandleSuccess("Thao tác");
                    //send a message about the approved
                    MessagingCenter.Send(this, "report_approved", _model);

                    await FetchReportByIdAsync();

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

        private void Reporter()
        {
            Navigation.NavigateTo($"/hirers/details/{_model.UserId}");

        }
        private void Reported()
        {
            Navigation.NavigateTo($"/hirers/details/{_model.ToUserId}");

        }
    }
}