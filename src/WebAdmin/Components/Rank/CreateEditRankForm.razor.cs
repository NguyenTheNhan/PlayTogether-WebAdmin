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
using WebAdmin.Client.Services.Exceptions;

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
        private string _errorMessage = string.Empty;

        private async Task AddRankAsync()
        {
            _errorMessage = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(_name))
                {
                    _errorMessage = "Name is required";
                    return;
                }if (_no<=0)
                {
                    _errorMessage = "No must more than 0";
                    return;
                }
                _isBusy = true;
                //Call Api to add ToDo Item
                var result = await RankService.CreateAsync(_no,_name, GameId);
                _name = string.Empty;

                //Notify the parent about the newly added item
                //await OnRankAdded.InvokeAsync(result.Value);
            }
            catch (ApiException ex)
            {
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