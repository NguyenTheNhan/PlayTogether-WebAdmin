using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;

namespace WebAdmin.Components
{
    public partial class TrainModel
    {
        [Inject]
        public ISystemConfigService SystemConfigService { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy { get; set; } = false;
        private float _rmse { get; set; } = 0;
        private float _rsq { get; set; } = 0;
        private string _errorMessage = string.Empty;

        private async Task TrainModelAsync()
        {
            _errorMessage = "";

            try
            {
                var result = await SystemConfigService.TrainModelRecommendation();
                _rmse = result.Content.Item1;
                _rsq = result.Content.Item2;
                StateHasChanged();

                //  MessagingCenter.Send(this, "maintain", result.Content);
                Error.HandleSuccess("Train model thành công");
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error.HandleError(ex);

            }
        }

    }
}