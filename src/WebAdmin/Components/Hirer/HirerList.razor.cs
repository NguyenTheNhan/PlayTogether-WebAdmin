using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Hirer;

namespace WebAdmin.Components
{
    public partial class HirerList
    {
        [Inject]
        public IHirerService HirerService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private List<HirerSummary> _hirers = new();

        private async Task<IEnumerable<HirerSummary>> GetHirersAsync(string query = "", string status = "", bool? isActive = null, int pageNumber = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await HirerService.GetHirersAsync(query, status, isActive, pageNumber, pageSize);
                _hirers = result.Content.ToList();

                return result.Content;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }
            _isBusy = false;
            return null;
        }

        #region Edit
        private async Task EditHirer(HirerSummary hirer)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", hirer.IsActive ? $"Bạn có muốn khoá tài khoản '{hirer.Email}'?" : $"Bạn có muốn mở khoá tài khoản '{hirer.Email}'?");
            parameters.Add("ButtonText", hirer.IsActive ? "Khoá" : "Mở khoá");
            parameters.Add("Color", hirer.IsActive ? Color.Error : Color.Success);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>(hirer.IsActive ? "Khoá tài khoản" : "Mở khoá tài khoản", parameters, options);

            var confirmationResult = await dialog.Result;
            if (!confirmationResult.Cancelled)
            {
                //Confirm to active/unactive
                try
                {
                    // await HirerService.ActiveAsync(hirer.Id, !hirer.IsActive);
                    await HirerService.ActiveAsync(hirer.Id, !hirer.IsActive, 0, DateTime.Now);

                    //success
                    Error.HandleSuccess(hirer.IsActive ? "Khoá tài khoản" : "Mở khoá tài khoản");
                    //send a message about the active/unactive
                    MessagingCenter.Send(this, "hirer_locked", hirer);
                }
                catch (ApiException ex)
                {
                    //TODO: log this error
                    _errorMessage = ex.ApiErrorResponse.Message;
                }
                catch (Exception ex)
                {
                    //TODO: Log this error

                    Error.HandleError(ex);
                }
            }
        }
        #endregion




        #region View
        private void ViewHirer(HirerSummary hirer)
        {
            Navigation.NavigateTo($"/hirers/details/{hirer.Id}");
        }
        #endregion
    }
}