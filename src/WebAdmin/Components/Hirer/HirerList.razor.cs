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
                _hirers = result.ToList();

                return result;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }
            _isBusy = false;
            return null;
        }

        #region Edit
        private void EditHirer(HirerSummary hirer)
        {
            Navigation.NavigateTo($"/hirers/form/{hirer.Id}");
        }
        #endregion

    }
}