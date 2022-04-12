using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Charities;

namespace WebAdmin.Components
{
    public partial class CharityInfo
    {
        [Inject]
        public ICharitiesService CharitiesService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }


        private CharitiesSummary _model = new CharitiesSummary();
        private bool _isBusy = false;
        // private string _action = "Khoá 1 ngày";
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {

            await FetchCharitiesByIdAsync();
        }



        private async Task ActiveAsync()
        {
            _isBusy = true;
            var parameters = new DialogParameters();
            parameters.Add("ContentText", _model.IsActive ? $"Bạn có muốn khoá tài khoản '{_model.OrganizationName}'?" : $"Bạn có muốn mở khoá tài khoản '{_model.OrganizationName}'?");
            parameters.Add("ButtonText", _model.IsActive ? "Khoá" : "Mở khoá");
            parameters.Add("Color", _model.IsActive ? Color.Error : Color.Success);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>(_model.IsActive ? "Khoá tài khoản" : "Mở khoá tài khoản", parameters, options);

            var confirmationResult = await dialog.Result;
            if (!confirmationResult.Cancelled)
            {
                try
                {

                    await CharitiesService.ActiveAsync(_model.Id, !_model.IsActive);

                    //success
                    Error.HandleSuccess(_model.IsActive ? "Khoá tài khoản" : "Mở khoá tài khoản");
                    await FetchCharitiesByIdAsync();

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

        private async Task FetchCharitiesByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await CharitiesService.GetByIdAsync(Id);
                _model = result;

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
    }
}