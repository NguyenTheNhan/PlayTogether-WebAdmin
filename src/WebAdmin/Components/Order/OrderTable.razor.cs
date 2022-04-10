using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Order;

namespace WebAdmin.Components
{
    public partial class OrderTable
    {
        [Inject]
        public IOrderService OrderService { get; set; }


        [Parameter]
        public EventCallback<OrderDetail> OnViewClicked { get; set; }



        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; }

        private string _query { get; set; } = string.Empty;
        private string _status { get; set; } = string.Empty;
        private DateTime? _fromDate { get; set; } = DateTime.Parse("1/1/0001");
        private DateTime? _toDate { get; set; } = DateTime.Now;

        private MudTable<OrderDetail> _table;



        private async Task<TableData<OrderDetail>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await OrderService.GetOrdersAsync(UserId, _status, _fromDate, _toDate, state.Page + 1, state.PageSize);

                return new TableData<OrderDetail>
                {
                    Items = result.Content,
                    TotalItems = result.TotalCount,
                };
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }

            return new TableData<OrderDetail>
            {
                Items = new List<OrderDetail>(),
                TotalItems = 0
            };
        }

        private void OnSearch(string query, string status, DateTime? fromDate, DateTime? toDate)
        {
            _query = query;
            _status = status;
            _fromDate = fromDate;
            _toDate = toDate;
            _table.ReloadServerData();
        }
    }
}