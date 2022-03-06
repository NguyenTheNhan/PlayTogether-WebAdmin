using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Components
{
    public partial class TypeOfGameItem
    {
        [Inject]
        public ITypeOfGameService TypeOfGameService { get; set; }

        [Parameter]
        public TypeOfGameWithGameTypeDetail Item { get; set; }
        [Parameter]
        public string GameId { get; set; }
        [Parameter]
        public EventCallback<TypeOfGameWithGameTypeDetail> OnItemDeleted { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        public bool _isBusy = false;

        private string _errorMessage = string.Empty;

        private async Task RemoveItemAsync()
        {
            try
            {
                _isBusy = true;
                //Call Api to add ToDo Item
                await TypeOfGameService.DeleteAsync(Item.Id);


                //Notify the parent about the newly added item
                await OnItemDeleted.InvokeAsync(Item);
            }
            catch (ApiException ex)
            {
                //TODO: Handle error globally
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
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