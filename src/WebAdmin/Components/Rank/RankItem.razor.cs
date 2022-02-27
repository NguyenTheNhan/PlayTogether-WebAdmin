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
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.Rank;

namespace WebAdmin.Components
{
    public partial class RankItem
    {
        [Inject]
        public IRankService RankService { get; set; }

        [Parameter]
        public RankDetail Item { get; set; }
        [Parameter]
        public string GameId { get; set; }
        [Parameter]
        public EventCallback<RankDetail> OnItemDeleted { get; set; }
        [Parameter]
        public EventCallback<RankDetail> OnItemEdited { get; set; }
        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isCheck = true;

        private bool _isEditMode = false;

        public bool _isBusy = false;

        private string _name = string.Empty;
        private int _no = 0;
        private string _errorMessage = string.Empty;

        private void ToggleEditmode(bool isCancel)
        {
            _errorMessage = string.Empty;
            if (_isEditMode)
            {
                _isEditMode = false;
                _name = isCancel ? Item.Name : _name;
                _no = isCancel ? Item.No : _no;

            }
            else
            {
                _isEditMode = true;
                _name = Item.Name;
                _no = Item.No;
            }

        }
        private async Task EditItemAsync()
        {
            _errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(Item.Name))
                {
                    _errorMessage = "Name is required";
                    return;
                }
                if (Item.No <= 0)
                {
                    _errorMessage = "No must more than 0";
                    return;
                }
                _isBusy = true;
                //Call Api to edit ToDo Item
                var result = await RankService.EditAsync(Item.Id, Item.No, Item.Name, GameId);

                //Notify the parent about the edited item
                await OnItemEdited.InvokeAsync(result);
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