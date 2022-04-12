using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Charities;

namespace WebAdmin.Components
{
    public partial class CharitiesTable
    {
        [Inject]
        public ICharitiesService CharitiesService { get; set; }


        [Parameter]
        public EventCallback<CharitiesSummary> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<CharitiesSummary> OnEditClicked { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; }

        private string _errorMessage { get; set; } = string.Empty;
        private string _query = string.Empty;

        private bool _isActive { get; set; } = true;
        private MudTable<CharitiesSummary> _table;

        //protected override void OnInitialized()
        //{
        //    MessagingCenter.Subscribe<CharitiesList, CharitiesSummary>(this, "charity_deleted", async (sender, args) =>
        //    {
        //        await _table.ReloadServerData();
        //        StateHasChanged();
        //    });
        //}

        private async Task<TableData<CharitiesSummary>> ServerReloadAsync(TableState state)
        {
            try
            {
                var result = await CharitiesService.GetCharitiesAsync(_query, _isActive, state.Page + 1, state.PageSize);

                return new TableData<CharitiesSummary>
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

            return new TableData<CharitiesSummary>
            {
                Items = new List<CharitiesSummary>(),
                TotalItems = 0
            };
        }

        private void OnSearch(string query, bool isActive)
        {
            _query = query;
            _isActive = isActive;
            _table.ReloadServerData();
        }
    }
}