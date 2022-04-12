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
using WebAdmin.Shared.Models.Order;

namespace WebAdmin.Components
{
    public partial class OrderList
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private List<OrderDetail> _orders = new();

        protected override async Task OnInitializedAsync()
        {
            _isBusy = true;
            await GetOrderAsync();
            _isBusy = false;
        }
        private async Task<PagedList<OrderDetail>> GetOrderAsync()
        {
            _isBusy = true;
            try
            {
                var result = await OrderService.GetOrdersAsync(UserId);
                _orders = result.Content.ToList();
                return result;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
            }
            catch (Exception ex)
            {
                //TODO: log this error
                Error.HandleError(ex);
            }
            _isBusy = false;
            return null;
        }


        #region View
        private void ViewOrder(OrderDetail order)
        {
            //Navigation.NavigateTo($"/hirers/order/{order.Id}");

            var parameters = new DialogParameters();
            parameters.Add("Id", order.Id);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<OrderDetails>("Thông tin thuê", parameters, options);
        }
        #endregion



    }
}