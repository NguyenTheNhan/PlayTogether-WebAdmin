using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Game;

namespace WebAdmin.Components
{
    public partial class GameTable
    {
        [Inject]
        public IGameService GameService { get; set; }


        [Parameter]
        public EventCallback<GameSummary> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<GameSummary> OnEditClicked { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; }

        private MudTable<GameSummary> _table;
        private string _query = string.Empty;

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<GameList, GameSummary>(this, "game_deleted", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
        }

        private async Task<TableData<GameSummary>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await GameService.GetGamesAsync(_query, state.Page + 1, state.PageSize);
                var tmp = await GameService.GetGamesAsync(_query, 0, 1000);

                return new TableData<GameSummary>
                {
                    Items = result,
                    TotalItems = tmp.Count()
                };
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }

            return new TableData<GameSummary>
            {
                Items = new List<GameSummary>(),
                TotalItems = 0
            };
        }

        private void OnSearch(string query)
        {
            _query = query;
            _table.ReloadServerData();
        }
    }
}