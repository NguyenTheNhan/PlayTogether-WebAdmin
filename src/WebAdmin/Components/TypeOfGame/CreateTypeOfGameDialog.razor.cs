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
    public partial class CreateTypeOfGameDialog
    {
        [Inject]
        public IGameService GameService { get; set; }
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
        [Inject]
        public IGameTypeService GameTypeService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }


        [Parameter]
        public GameDetail _model { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }


        private List<TypeOfGameWithGameTypeDetail> _typeOfGames = new();

        private List<RankDetail> _ranks = new();

        private List<GameTypeSummary> _gameTypes = new();

        private bool _isBusy = false;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;


        protected override async Task OnInitializedAsync()
        {

            await FetchGameTypeAsync();
        }

        private void Close()
        {
            MudDialog.Cancel();
        }


        private void OnItemAddedCallBack(TypeOfGameWithGameTypeDetail item)
        {
            _typeOfGames.Add(item);

        }


        private async Task FetchGameTypeAsync()
        {
            _isBusy = true;

            try
            {
                var result = await GameTypeService.GetGameTypesAsync("", 1, 100);
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
    }
}