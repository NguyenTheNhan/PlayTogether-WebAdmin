using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Feedback;

namespace WebAdmin.Components
{
    public partial class FeedbackList
    {
        [Inject]
        public IFeedbackService FeedbackService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private List<FeedbackSummary> _reports = new();



        #region View
        private void ViewFeedback(FeedbackSummary report)
        {
            //Navigation.NavigateTo($"/hirers/order/{order.Id}");

            var parameters = new DialogParameters();
            parameters.Add("Id", report.Id);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<FeedbackDetailDialog>("Thông tin report", parameters, options);
        }
        #endregion
    }
}