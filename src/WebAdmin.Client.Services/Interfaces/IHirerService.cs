using System;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Hirer;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IHirerService
    {
        Task<PagedList<HirerSummary>> GetHirersAsync(string query = null, string status = null, bool? isActive = null, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<UserDetail>> GetByIdAsync(string id);

        Task<ApiResponse<UserDetail>> ActiveAsync(string id, bool isActive, int numDateDisable, DateTime? dateDisable);

    }
}
