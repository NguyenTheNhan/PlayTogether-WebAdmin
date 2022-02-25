using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using WebAdmin;
using WebAdmin.Shared;
using WebAdmin.Components;
using MudBlazor;
using Blazored.FluentValidation;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.Game;
using AKSoftware.Blazor.Utilities;

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

        private string _query = string.Empty;
        private MudTable<GameSummary> _table;

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
                var result = await GameService.GetGamesAsync(_query, state.Page, state.PageSize);

                return new TableData<GameSummary>
                {
                    Items = result,
                    TotalItems = result.Count()
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