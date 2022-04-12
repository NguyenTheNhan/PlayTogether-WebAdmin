using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Hirer;

namespace WebAdmin.Components
{
    public partial class UserInfo
    {
        [Inject]
        public IHirerService UserService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }


        private UserDetail _model = new UserDetail();
        private bool _isBusy = false;
        private string _action = "Khoá 1 ngày";
        private int _numDateDisable = 1;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {

            await FetchUserByIdAsync();
        }

        private async void selectOption(int option)
        {
            _numDateDisable = option;
            if (option <= 3)
            {
                _action = "khoá " + option + " ngày";

            }
            else
            {
                _action = "khoá vĩnh viễn";
            }
        }


        private async Task ActiveAsync()
        {
            _isBusy = true;
            var parameters = new DialogParameters();
            parameters.Add("ContentText", _model.IsActive ? $"Bạn có muốn {_action} '{_model.Name}'?" : $"Bạn có muốn mở khoá tài khoản '{_model.Name}' ?");
            parameters.Add("ButtonText", _model.IsActive ? "Khoá" : "Mở khoá");
            parameters.Add("Color", _model.IsActive ? Color.Error : Color.Success);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>(_model.IsActive ? "Khoá tài khoản" : "Mở khoá tài khoản", parameters, options);

            var confirmationResult = await dialog.Result;
            if (!confirmationResult.Cancelled)
            {
                try
                {

                    await UserService.ActiveAsync(_model.Id, !_model.IsActive, _numDateDisable, DateTime.Now);

                    //success
                    Error.HandleSuccess(_model.IsActive ? "Khoá tài khoản" : "Mở khoá tài khoản");
                    await FetchUserByIdAsync();

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
            _isBusy = false;

        }

        private async Task FetchUserByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await UserService.GetByIdAsync(Id);
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


    }
}