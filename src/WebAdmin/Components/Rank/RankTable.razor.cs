using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Rank;

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
        public EventCallback<RankDetail> OnAddClicked { get; set; }

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
            MessagingCenter.Subscribe<RankItem, RankDetail>(this, "rank_edited", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
            MessagingCenter.Subscribe<CreateEditRankForm, RankDetail>(this, "rank_added", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });

        }

        private async Task<TableData<RankDetail>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await RankService.GetRankAsync(GameId, state.Page + 1, state.PageSize);
                var tmp = await RankService.GetRankAsync(GameId, 0, 1000);

                return new TableData<RankDetail>
                {
                    Items = result,
                    TotalItems = tmp.Count(),
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