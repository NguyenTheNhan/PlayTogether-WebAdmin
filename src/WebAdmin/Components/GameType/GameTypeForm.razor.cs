using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.GameType;

namespace WebAdmin.Components
{
    public partial class GameTypeForm
    {
        [Inject]
        public IGameTypeService GameTypeService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isEditMode => Id != null;

        private GameTypeSummary _model = new GameTypeSummary();
        private bool _isBusy = false;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (_isEditMode)
                await FetchGameTypeByIdAsync();
        }

        private async Task SubmitFormAsync()
        {
            _isBusy = true;
            try
            {
                if (_isEditMode)
                {
                    await GameTypeService.EditAsync(_model);
                    Error.HandleSuccess("Chỉnh sửa");
                }
                else
                {
                    await GameTypeService.CreateAsync(_model);
                    Error.HandleSuccess("Thêm mới thể loại");
                }
                //success
                Navigation.NavigateTo("/gametypes");
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error.HandleError(ex);

            }

            _isBusy = false;

        }

        private async Task FetchGameTypeByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await GameTypeService.GetByIdAsync(Id);
                _model = result.Content;

            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                //TODO: log the error
                Error.HandleError(ex);

            }

            _isBusy = false;
        }
    }
}