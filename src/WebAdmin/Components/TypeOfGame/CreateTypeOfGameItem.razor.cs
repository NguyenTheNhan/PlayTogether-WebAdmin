using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.GameType;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Components
{
    public partial class CreateTypeOfGameItem
    {
        [Inject]
        public ITypeOfGameService TypeOfGameService { get; set; }
        [Parameter]
        public string GameId { get; set; }
        [Parameter]
        public List<GameTypeSummary> GameTypes { get; set; }

        [Parameter]
        public EventCallback<TypeOfGameWithGameTypeDetail> OnTypeOfGameAdded { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private GameTypeSummary model { get; set; }

        private string _errorMessage = string.Empty;


        private async Task AddTypeOfGameItemAsync()
        {
            _errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(model.Id))
                {
                    _errorMessage = "GameType is required";
                    return;
                }
                _isBusy = true;
                //Call Api to add TypeOfGame Item
                await TypeOfGameService.CreateAsync(GameId, model.Id);

                Error.HandleSuccess("Thêm thể loại cho game");
                //Notify the parent about the newly added item
                await OnTypeOfGameAdded.InvokeAsync();
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {

                //TypeOfGame: Handle error globally
                Error.HandleError(ex);
            }
            _isBusy = false;
        }

        private async Task<IEnumerable<GameTypeSummary>> Search1(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return GameTypes;
            return GameTypes.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)
                                      || x.OtherName.Contains(value, StringComparison.InvariantCultureIgnoreCase)
                                      || x.ShortName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}