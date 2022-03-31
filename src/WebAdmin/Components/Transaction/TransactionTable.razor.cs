using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Transaction;

namespace WebAdmin.Components
{
    public partial class TransactionTable
    {
        [Inject]
        public ITransactionService TransactionService { get; set; }


        //[Parameter]
        //public EventCallback<TransactionSummary> OnViewClicked { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; }

        private string _type { get; set; } = string.Empty;
        private DateTime? _fromDate { get; set; } = DateTime.Parse("1/1/2000");
        private DateTime? _toDate { get; set; } = DateTime.Now;

        private MudTable<TransactionSummary> _table;



        private async Task<TableData<TransactionSummary>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await TransactionService.GetTransactionsAsync(UserId, _type, _fromDate, _toDate, state.Page, state.PageSize);
                var tmp = await TransactionService.GetTransactionsAsync(UserId, _type, _fromDate, _toDate, 0, 1000);

                return new TableData<TransactionSummary>
                {
                    Items = result,
                    TotalItems = tmp.Count(),
                };
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }

            return new TableData<TransactionSummary>
            {
                Items = new List<TransactionSummary>(),
                TotalItems = 0
            };
        }

        private void OnSearch(string type, DateTime? fromDate, DateTime? toDate)
        {
            _type = type;
            _fromDate = fromDate;
            _toDate = toDate;
            _table.ReloadServerData();
        }
    }
}