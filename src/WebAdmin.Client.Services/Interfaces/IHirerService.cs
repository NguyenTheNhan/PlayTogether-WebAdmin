﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Hirer;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IHirerService
    {
        Task<IEnumerable<HirerSummary>> GetHirersAsync(string query = null, string status = null, bool? isActive = null, int pageNumber = 1, int pageSize = 10);
        Task<HirerSummary> GetByIdAsync(string id);

        Task<HirerSummary> ActiveAsync(string id, bool isActive, string message);

    }
}