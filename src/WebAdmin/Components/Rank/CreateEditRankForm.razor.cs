using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;

namespace WebAdmin.Components
{
    public partial class CreateEditRankForm
    {
        [Inject]
        public IRankService RankService { get; set; }
        [Parameter]
        public string GameId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _name { get; set; }
        private int _no { get; set; }
        private string _errorMessage { get; set; } = string.Empty;

        private async Task AddRankAsync()
        {
            _errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(_name))
                {
                    _errorMessage = "Name is required";
                    return;
                }
                if (_no < 0)
                {
                    _errorMessage = "No must more than 0";
                    return;
                }
                _isBusy = true;
                //Call Api to add Rank Item
                var result = await RankService.CreateAsync(_no, _name, GameId);
                _name = string.Empty;
                _no = 0;
                if (result.IsSuccess == true)
                {
                    MessagingCenter.Send(this, "rank_added", result.Content);
                    Error.HandleSuccess("Thêm mới");
                    //_errorMessage = string.Empty;
                }
                else
                {

                    _errorMessage = result.Error.Message;
                    Error.HandleError("Không thể thêm mới rank");
                }

                //Notify the parent about the newly added item
                //await OnRankAdded.InvokeAsync(result.Value);
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
            }
            catch (Exception ex)
            {

                //TODO: Handle error globally
                Error.HandleError(ex);
            }
            _isBusy = false;
        }
    }
}