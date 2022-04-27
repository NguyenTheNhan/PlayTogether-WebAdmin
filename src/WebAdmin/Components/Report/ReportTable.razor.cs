using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Report;

namespace WebAdmin.Components
{
    public partial class ReportTable
    {
        [Inject]
        public IReportService ReportService { get; set; }


        [Parameter]
        public EventCallback<ReportSummary> OnViewClicked { get; set; }



        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; } = false;

        private int tmp { get; set; } = 0;
        private bool isMany { get; set; } = false;
        private bool? _isApprove { get; set; } = null;
        private DateTime? _fromDate { get; set; } = DateTime.Parse("1/1/0001");
        private DateTime? _toDate { get; set; } = DateTime.Now;

        private MudTable<ReportSummary> _table;

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<ReportDetailDialog, ReportDetails>(this, "report_disapproved", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
            MessagingCenter.Subscribe<ProcessApproveDialog, string>(this, "report_approved", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });

        }

        private async Task<TableData<ReportSummary>> ServerReloadAsync(TableState state)
        {
            _isBusy = true;
            try
            {
                var result = await ReportService.GetReportsAsync(_isApprove, _fromDate, _toDate, state.Page + 1, state.PageSize);
                if (result.TotalCount > 6) isMany = true;
                _isBusy = false;
                return new TableData<ReportSummary>
                {
                    Items = result.Content,
                    TotalItems = result.TotalCount,
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