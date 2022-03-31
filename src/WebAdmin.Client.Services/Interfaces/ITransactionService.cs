using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Transaction;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionSummary>>
            GetTransactionsAsync(string userId,
                           string type = null,
                           DateTime? fromDate = null,
                           DateTime? toDate = null,
                           int pageNumber = 1,
                           int pageSize = 1000);
    }
}
