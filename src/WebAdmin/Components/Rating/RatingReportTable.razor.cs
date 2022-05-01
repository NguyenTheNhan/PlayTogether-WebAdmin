using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Rating;

namespace WebAdmin.Components
{
    public partial class RatingReportTable
    {
        [Inject]
        public IRatingService RatingService { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; } = false;

        private int tmp { get; set; } = 0;
        private bool isMany { get; set; } = false;
        private bool? _isApprove { get; set; } = null;
        private bool? _isViolate { get; set; } = true;
        private string _errorMessage { get; set; } = string.Empty;

        private MudTable<RatingDetail> _table;

        private string reporter { get; set; }

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<RatingReportDialog, RatingDetail>(this, "rating_approved", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });

        }


        private async Task<TableData<RatingDetail>> ServerReloadAsync(TableState state)
        {
            _isBusy = true;
            try
            {
                var result = await RatingService.GetRatingsAsync(_isApprove, _isViolate, state.Page + 1, state.PageSize);
                //if (result.TotalCount > 6) isMany = true;
                _isBusy = false;
                return new TableData<RatingDetail>
                {
                    Items = result.Content,
                    TotalItems = result.TotalCount,
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

            return new TableData<RatingDetail>
            {
                Items = new List<RatingDetail>(),
                TotalItems = 0
            };
        }

        private void OnSearch(int tmp)
        {
            switch (tmp)
            {
                case 0:
                    _isApprove = null;
                    _isViolate = true;
                    break;
                case 1:
                    _isApprove = true;
                    _isViolate = true;
                    break;
                case 2:
                    _isApprove = false;
                    _isViolate = null;
                    break;
            }

            _table.ReloadServerData();
        }


        private void ViewRating(RatingDetail rating)
        {

            var parameters = new DialogParameters();
            parameters.Add("Rating", rating);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<RatingReportDialog>("Thông tin báo cáo", parameters, options);
        }

    }
}