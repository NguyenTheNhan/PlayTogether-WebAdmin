using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Player;

namespace WebAdmin.Components
{
    public partial class PlayerTable
    {
        [Inject]
        public IPlayerService PlayerService { get; set; }


        [Parameter]
        public EventCallback<PlayerSummary> OnEditClicked { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; }

        private string _query = string.Empty;
        private string _status { get; set; } = string.Empty;
        private bool _isActive { get; set; }
        private MudTable<PlayerSummary> _table;

        //protected override void OnInitialized()
        //{
        //    MessagingCenter.Subscribe<PlayerList, PlayerSummary>(this, "player_deleted", async (sender, args) =>
        //    {
        //        await _table.ReloadServerData();
        //        StateHasChanged();
        //    });
        //}


        private async Task<TableData<PlayerSummary>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await PlayerService.GetPlayersAsync(_query, _status, _isActive, state.Page, state.PageSize);

                return new TableData<PlayerSummary>
                {
                    Items = result,
                    TotalItems = result.Count()
                };
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }

            return new TableData<PlayerSummary>
            {
                Items = new List<PlayerSummary>(),
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