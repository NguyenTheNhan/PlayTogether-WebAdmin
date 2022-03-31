using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
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


        private async Task<IEnumerable<OrderDetail>> GetOrderAsync()
        {
            _isBusy = true;
            try
            {
                var result = await OrderService.GetOrdersAsync(UserId);
                _orders = result.ToList();
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
            _isBusy = false;
            return null;
        }


        #region View
        private void ViewOrder(OrderDetail Order)
        {

        }
        #endregion



    }
}