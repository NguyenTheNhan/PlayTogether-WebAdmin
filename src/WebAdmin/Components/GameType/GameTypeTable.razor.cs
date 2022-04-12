using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.GameType;

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
        public bool _isBusy { get; set; }

        private string _errorMessage { get; set; } = string.Empty;
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
                var result = await GameTypeService.GetGameTypesAsync(_query, state.Page + 1, state.PageSize);

                return new TableData<GameTypeSummary>
                {
                    Items = result.Content,
                    TotalItems = result.TotalCount
                };
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
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