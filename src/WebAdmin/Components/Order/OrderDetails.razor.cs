using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Hirer;
using WebAdmin.Shared.Models.Order;

namespace WebAdmin.Components
{
    public partial class OrderDetails
    {
        [Inject]
        public IOrderService OrderService { get; set; }
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


        private OrderDetail _model = new();
        private UserDetail _hirer = new();
        private UserDetail _player = new();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private float _rating = 0;

        protected override async Task OnInitializedAsync()
        {
            await FetchOrderByIdAsync();

        }

        private void Close()
        {
            MudDialog.Cancel();
        }

        private async Task FetchOrderByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await OrderService.GetByIdAsync(Id);

                _model = result;
                var hirer = await HirerService.GetByIdAsync(_model.UserId);
                var player = await HirerService.GetByIdAsync(_model.ToUserId);
                _hirer = hirer;
                _player = player;
                _rating = _model.Ratings.Count() != 0 ? _model.Ratings.FirstOrDefault().Rate : 0;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //TODO: log the error
                Error.HandleError(ex);

            }

            _isBusy = false;
        }

        private void UserDetail()
        {
            Navigation.NavigateTo($"/hirers/details/{_model.UserId}");

        }
        private void PlayerDetail()
        {
            Navigation.NavigateTo($"/hirers/details/{_model.ToUserId}");

        }
    }
}











