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
using WebAdmin.Shared.Models;
using AKSoftware.Blazor.Utilities;

namespace WebAdmin.Components
{
    public partial class GameTypeTable
    {
        [Inject]
        public IGameTypeService GameTypeService { get; set; }


        [Parameter]
        public EventCallback<GameTypeSummary> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<GameTypeSummary> OnEditClicked { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private string _query = string.Empty;
        private MudTable<GameTypeSummary> _table;

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<GameTypeList, GameTypeSummary>(this, "gameType_deleted", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
        }

        private async Task<TableData<GameTypeSummary>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await GameTypeService.GetGameTypesAsync(_query, state.Page, state.PageSize);

                return new TableData<GameTypeSummary>
                {
                    Items = result,
                    TotalItems = result.Count()
                };
            }
            catch (Exception ex)
            {
               Error.HandleError(ex);
            }

            return new TableData<GameTypeSummary>
            {
                Items = new List<GameTypeSummary>(),
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