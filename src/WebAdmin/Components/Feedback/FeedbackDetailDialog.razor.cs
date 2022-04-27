using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Feedback;
using WebAdmin.Shared.Models.Hirer;

namespace WebAdmin.Components
{
    public partial class FeedbackDetailDialog
    {
        [Inject]
        public IFeedbackService FeedbackService { get; set; }
        [Inject]
        public IHirerService HirerService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public FeedbackSummary Feedback { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }


        private FeedbackDetail _model = new();
        private UserDetail user = new();
        private bool _approve { get; set; } = true;
        private string _action = "Duyệt";
        private bool _isBusy { get; set; } = false;
        private string _errorMessage = string.Empty;
        private string _type { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await FetchFeedbackByIdAsync();
            if (_model.TypeOfFeedback.Equals("Service"))
            {
                _type = "Dịch vụ";
            }
            else if (_model.TypeOfFeedback.Equals("Suggest"))
            {
                _type = "Đề xuất";
            }
            else if (_model.TypeOfFeedback.Equals("SystemError"))
            {
                _type = "Lỗi hệ thống";
            }
        }

        private void Close()
        {
            MudDialog.Cancel();
        }

        private async Task FetchFeedbackByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await FeedbackService.GetByIdAsync(Feedback.Id);

                _model = result.Content;
                //  user = await HirerService.GetByIdAsync(_model.UserId);

            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
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

            var parameters = new DialogParameters();
            parameters.Add("ContentText", _model.IsApprove == -1 ? (_approve ? "Bạn muốn duyệt đề xuất này?" : "Bạn không duyệt đề xuất này ?")
                                                                   : (_model.IsApprove == 1 ? "Bạn không duyệt đề xuất này ?" : "Bạn muốn duyệt đề xuất này?"));
            parameters.Add("ButtonText", _model.IsApprove == -1 ? (_approve ? "Duyệt" : "Không duyệt")
                                                                  : (_model.IsApprove == 1 ? "Không duyệt" : "Duyệt"));
            parameters.Add("Color", Color.Primary);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Xác nhận", parameters, options);

            var confirmationResult = await dialog.Result;
            if (!confirmationResult.Cancelled)
            {
                try
                {
                    if (_model.IsApprove == 1)
                    {
                        await FeedbackService.ActiveAsync(Feedback.Id, 0);
                    }
                    else if (_model.IsApprove == 0)
                    {
                        await FeedbackService.ActiveAsync(Feedback.Id, 1);
                    }
                    else
                    {
                        await FeedbackService.ActiveAsync(Feedback.Id, _approve ? 1 : 0);
                    }

                    //success
                    Error.HandleSuccess("Thao tác thành công");
                    //send a message about the approved
                    MessagingCenter.Send(this, "feedback_approved", _model);

                    await FetchFeedbackByIdAsync();

                }
                catch (ApiException ex)
                {
                    _errorMessage = ex.ApiErrorResponse.Message;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Error.HandleError(ex);

                }
            }


        }

        private void User()
        {
            Navigation.NavigateTo($"/hirers/details/{_model.UserId}");

        }
    }
}