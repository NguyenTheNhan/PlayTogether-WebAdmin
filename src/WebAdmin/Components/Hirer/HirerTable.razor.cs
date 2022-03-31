using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Hirer;

namespace WebAdmin.Components
{
    public partial class HirerTable
    {
        [Inject]
        public IHirerService HirerService { get; set; }


        [Parameter]
        public EventCallback<HirerSummary> OnEditClicked { get; set; }
        [Parameter]
        public EventCallback<HirerSummary> OnViewClicked { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; }

        private string _query = string.Empty;
        private string _status { get; set; } = string.Empty;
        private bool _isActive { get; set; } = true;
        private MudTable<HirerSummary> _table;

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<HirerList, HirerSummary>(this, "hirer_locked", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
        }


        private async Task<TableData<HirerSummary>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await HirerService.GetHirersAsync(_query, _status, _isActive, state.Page + 1, state.PageSize);
                var tmp = await HirerService.GetHirersAsync(_query, _status, _isActive, 0, 1000);
                return new TableData<HirerSummary>
                {
                    Items = result,
                    TotalItems = tmp.Count()
                };
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }

            return new TableData<HirerSummary>
            {
                Items = new List<HirerSummary>(),
                TotalItems = 0
            };
        }

        private void OnSearch(string query, string status, bool isActive)
        {
            _query = query;
            _status = status;
            _isActive = isActive;
            _table.ReloadServerData();
        }
    }
}