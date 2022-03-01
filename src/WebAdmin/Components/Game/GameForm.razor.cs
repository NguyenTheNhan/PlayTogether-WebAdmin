using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using WebAdmin;
using WebAdmin.Shared;
using WebAdmin.Components;
using MudBlazor;
using Blazored.FluentValidation;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.Game;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Shared.Models.TypeOfGame;
using WebAdmin.Shared.Models.Rank;
using WebAdmin.Shared.Models.GameType;

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
            await FetchGameTypeAsync();
        }


        private async Task SubmitFormAsync()
        {
            _isBusy = true;
            try
            {
                if (_isEditMode)
                {
                    await GameService.EditAsync(_model);
                }
                else
                {
                    var result = await GameService.CreateAsync(_model);
                }
                //success
                Navigation.NavigateTo("/games");
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error.HandleError(ex);

            }

            _isBusy = false;

        }

        private void  OnItemAddedCallBack(TypeOfGameWithGameTypeDetail item)
        {
            _typeOfGameWithGameTypes.Add(item);

        }
        private void OnItemDeletedCallBack(TypeOfGameWithGameTypeDetail item)
        {
            _typeOfGameWithGameTypes.Remove(item);

        }

        private async Task FetchGameByIdAsync()
        {
            _isBusy = true;

            try
            {
                var result = await GameService.GetByIdAsync(Id);
                _model = result;
                _ranks = _model.ranks;
                _typeOfGameWithGameTypes = _model.typeOfGames;
                
                StateHasChanged();
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
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
                var result = await GameTypeService.GetGameTypesAsync("",1,100);
                _gameTypes = result.ToList();
                StateHasChanged();
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
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
