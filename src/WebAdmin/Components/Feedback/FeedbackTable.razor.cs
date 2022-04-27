using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Feedback;

namespace WebAdmin.Components
{
    public partial class FeedbackTable
    {
        [Inject]
        public IFeedbackService FeedbackService { get; set; }


        [Parameter]
        public EventCallback<FeedbackSummary> OnViewClicked { get; set; }



        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }
        public bool _isBusy { get; set; } = false;

        private bool _getAll { get; set; } = true;
        private int _isApprove { get; set; } = -1;
        private int tmp { get; set; } = 0;
        private string _type { get; set; } = string.Empty;
        private string _errorMessage { get; set; } = string.Empty;
        private DateTime? _fromDate { get; set; } = DateTime.Parse("0001-01-01");
        private DateTime? _toDate { get; set; } = DateTime.Now;

        private MudTable<FeedbackSummary> _table;

        protected override void OnInitialized()
        {
            MessagingCenter.Subscribe<FeedbackDetailDialog, FeedbackDetail>(this, "feedback_approved", async (sender, args) =>
            {
                await _table.ReloadServerData();
                StateHasChanged();
            });
        }

        private async Task<TableData<FeedbackSummary>> ServerReloadAsync(TableState state)
        {
            _isBusy = true;
            try
            {
                var result = await FeedbackService.GetFeedbacksAsync(_type, _isApprove, _getAll, _fromDate, _toDate, state.Page + 1, state.PageSize);
                _isBusy = false;
                return new TableData<FeedbackSummary>
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

            return new TableData<FeedbackSummary>
            {
                Items = new List<FeedbackSummary>(),
                TotalItems = 0
            };
        }

        private void OnSearch(string type, DateTime? fromDate, DateTime? toDate)
        {
            switch (tmp)
            {
                case 0:
                    _isApprove = -1;
                    _getAll = true;
                    break;
                case 1:
                    _isApprove = -1;
                    _getAll = false;
                    break;
                case 2:
                    _isApprove = 1;
                    _getAll = false;
                    break;
                case 3:
                    _isApprove = 0;
                    _getAll = false;
                    break;

            }
            _type = type;
            _fromDate = fromDate;
            _toDate = toDate;
            _table.ReloadServerData();
        }
    }
}