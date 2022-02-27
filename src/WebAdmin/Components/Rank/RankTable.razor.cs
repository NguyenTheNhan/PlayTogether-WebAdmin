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
using WebAdmin.Shared.Models.Rank;
using AKSoftware.Blazor.Utilities;

namespace WebAdmin.Components
{
    public partial class RankTable
    {
        [Inject]
        public IRankService RankService { get; set; }


        [Parameter]
        public EventCallback<RankDetail> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<RankDetail> OnViewClicked { get; set; }

        [Parameter]
        public string GameId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; }

        private MudTable<RankDetail> _table;

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<RankList, RankDetail>(this, "rank_deleted", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
        }

        private async Task<TableData<RankDetail>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await RankService.GetRankAsync(GameId);

                return new TableData<RankDetail>
                {
                    Items = result,
                    TotalItems = result.Count(),
                };
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }

            return new TableData<RankDetail>
            {
                Items = new List<RankDetail>(),
                TotalItems = 0
            };
        }

        //private void OnSearch(string query)
        //{
        //    _query = query;
        //    _table.ReloadServerData();
        //}
    }
}