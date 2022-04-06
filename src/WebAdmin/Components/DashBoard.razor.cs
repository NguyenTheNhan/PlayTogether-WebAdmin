using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models;

namespace WebAdmin.Components
{
    public partial class DashBoard
    {
        [Inject]
        public IDashBoardService DashBoardService { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }


        private DashBoardResponse dashBoard = new();
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {

            await GetDashBoardAsync();

        }
        public async void Reload()
        {
            await GetDashBoardAsync();
        }
        private async Task<DashBoardResponse> GetDashBoardAsync()
        {

            try
            {
                var result = await DashBoardService.GetDashBoard();
                dashBoard = result;
                return result;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //TODO: log this error
                Error.HandleError(ex);
            }

            return null;
        }
    }
}