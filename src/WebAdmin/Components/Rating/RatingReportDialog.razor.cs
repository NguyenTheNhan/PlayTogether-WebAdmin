using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Hirer;
using WebAdmin.Shared.Models.Rating;

namespace WebAdmin.Components
{
    public partial class RatingReportDialog
    {
        [Inject]
        public IRatingService RatingService { get; set; }
        [Inject]
        public IHirerService HirerService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public RatingDetail Rating { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }


        private RatingDetail _model = new();
        private UserDetail user = new();
        private bool _approve { get; set; } = true;
        private string _action = "Duyệt";
        private bool _isBusy { get; set; } = false;
        private string _errorMessage = string.Empty;
        private string _type { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await FetchRatingByIdAsync();

        }

        private void Close()
        {
            MudDialog.Cancel();
        }

        private async Task FetchRatingByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await RatingService.GetByIdAsync(Rating.Id);

                _model = result.Content;

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
                    if (_model.IsApprove == true)
                    {
                        await RatingService.ActiveAsync(Rating.Id, false);
                    }
                    else if (_model.IsApprove == false)
                    {
                        await RatingService.ActiveAsync(Rating.Id, true);
                    }
                    else
                    {
                        await RatingService.ActiveAsync(Rating.Id, _approve);
                    }

                    //success
                    Error.HandleSuccess("Thao tác");
                    //send a message about the approved
                    MessagingCenter.Send(this, "rating_approved", _model);

                    await FetchRatingByIdAsync();

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

        private void ViewOrder()
        {
            //Navigation.NavigateTo($"/hirers/order/{order.Id}");

            var parameters = new DialogParameters();
            parameters.Add("Id", _model.Order.Id);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<OrderDetails>("Thông tin thuê", parameters, options);
        }
    }
}