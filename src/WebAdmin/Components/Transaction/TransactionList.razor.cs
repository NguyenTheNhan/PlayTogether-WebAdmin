using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Transaction;

namespace WebAdmin.Components
{
    public partial class TransactionList
    {

        [Inject]
        public ITransactionService TransactionService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private List<TransactionSummary> _transactions = new();


        private async Task<IEnumerable<TransactionSummary>> GetTransactionAsync()
        {
            _isBusy = true;
            try
            {
                var result = await TransactionService.GetTransactionsAsync(UserId);
                _transactions = result.ToList();
                return result;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //TODO: log this error
                Error.HandleError(ex);
            }
            _isBusy = false;
            return null;
        }
    }
}


