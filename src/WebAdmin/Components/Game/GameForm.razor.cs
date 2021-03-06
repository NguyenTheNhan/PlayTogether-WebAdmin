using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Game;
using WebAdmin.Shared.Models.GameType;
using WebAdmin.Shared.Models.Rank;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Components
{
    public partial class GameForm
    {
        [Inject]
        public IGameService GameService { get; set; }
        [Inject]
        public IGameTypeService GameTypeService { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isEditMode => Id != null;

        private GameDetail _model = new GameDetail();

        private List<TypeOfGameSummary> _typeOfGames = new();
        private List<TypeOfGameWithGameTypeDetail> _typeOfGameWithGameTypes = new();

        private List<RankDetail> _ranks = new();

        private List<GameTypeSummary> _gameTypes = new();

        private bool _isBusy = false;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            if (_isEditMode)
                await FetchGameByIdAsync();

        }


        private async Task SubmitFormAsync()
        {
            _isBusy = true;
            try
            {
                if (_isEditMode)
                {
                    await GameService.EditAsync(_model);
                    Error.HandleSuccess("Chỉnh sửa thành công");
                }
                else
                {
                    var result = await GameService.CreateAsync(_model);
                    Error.HandleSuccess("Thêm mới game thành công");
                    Navigation.NavigateTo($"/games/form/{result.Content.Id}");
                }

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

            _isBusy = false;

        }

        private async void OnItemAddedCallBack(TypeOfGameWithGameTypeDetail item)
        {
            _typeOfGameWithGameTypes.Add(item);
            await FetchGameByIdAsync();
            StateHasChanged();

        }
        private async void OnItemDeletedCallBack(TypeOfGameWithGameTypeDetail item)
        {
            _typeOfGameWithGameTypes.Remove(item);
            await FetchGameByIdAsync();
            StateHasChanged();

        }

        private async Task FetchGameByIdAsync()
        {
            _isBusy = true;

            try
            {
                await FetchGameTypeAsync();
                var result = await GameService.GetByIdAsync(Id);
                _model = result.Content;
                _ranks = _model.ranks;
                _typeOfGameWithGameTypes = _model.typeOfGames;
                foreach (var typeOfGame in _typeOfGameWithGameTypes)
                {
                    foreach (var gametype in _gameTypes)
                    {
                        if (typeOfGame.GameType.Id.Contains(gametype.Id))
                        {
                            _gameTypes.Remove(gametype);
                            break;
                        }

                    }
                }
                StateHasChanged();
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
        private async Task FetchGameTypeAsync()
        {
            _isBusy = true;

            try
            {
                var result = await GameTypeService.GetGameTypesAsync("", 1, 1000);
                _gameTypes = result.Content.ToList();


                StateHasChanged();
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


        private void OnAddClicked()
        {
            var parameters = new DialogParameters();
            parameters.Add("_model", _model);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium };

            var dialog = DialogService.Show<CreateTypeOfGameDialog>("Details", parameters, options);

        }
    }
}
