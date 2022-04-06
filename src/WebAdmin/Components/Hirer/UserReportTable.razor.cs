using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Report;

namespace WebAdmin.Components
{
    public partial class UserReportTable
    {
        [Inject]
        public IReportService ReportService { get; set; }


        [Parameter]
        public EventCallback<ReportSummary> OnViewClicked { get; set; }



        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; }

        private int tmp { get; set; } = 0;
        private bool? _isApprove { get; set; } = null;
        private bool isMany { get; set; } = false;
        private DateTime? _fromDate { get; set; } = DateTime.Parse("1/1/0001");
        private DateTime? _toDate { get; set; } = DateTime.Now;

        private MudTable<ReportSummary> _table;





        private async Task<TableData<ReportSummary>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await ReportService.GetByUserIdAsync(UserId, _isApprove, _fromDate, _toDate, state.Page + 1, state.PageSize);
                var tmp = await ReportService.GetByUserIdAsync(UserId, _isApprove, _fromDate, _toDate, 0, 1000);
                if (tmp.Count() > 6) isMany = true;
                return new TableData<ReportSummary>
                {
                    Items = result,
                    TotalItems = tmp.Count(),
                };
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }

            return new TableData<ReportSummary>
            {
                Items = new List<ReportSummary>(),
                TotalItems = 0
            };
        }

        private void OnSearch(DateTime? fromDate, DateTime? toDate)
        {
            switch (tmp)
            {
                case 0:
                    _isApprove = null;
                    break;
                case 1:
                    _isApprove = true;
                    break;
                case 2:
                    _isApprove = false;
                    break;
            }
            _fromDate = fromDate;
            _toDate = toDate;
            _table.ReloadServerData();
        }
    }
}