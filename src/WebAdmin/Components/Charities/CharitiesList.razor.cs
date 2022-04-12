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
using WebAdmin.Shared.Models.Charities;

namespace WebAdmin.Components
{
    public partial class CharitiesList
    {
        [Inject]
        public ICharitiesService CharitiesService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private List<CharitiesSummary> _hirers = new();

        private async Task<PagedList<CharitiesSummary>> GetCharitiessAsync(string query = "", bool isActive = true, int pageNumber = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await CharitiesService.GetCharitiesAsync(query, isActive, pageNumber, pageSize);
                _hirers = result.Content.ToList();

                return result;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }
            _isBusy = false;
            return null;
        }

        #region Edit
        private async Task EditCharities(CharitiesSummary charity)
        {
            Navigation.NavigateTo($"/charities/details/{charity.Id}");
        }
        #endregion




        #region View
        private void ViewCharities(CharitiesSummary charity)
        {
            Navigation.NavigateTo($"/charities/details/{charity.Id}");
        }
        #endregion
    }
}
