using System;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Report;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IReportService
    {
        Task<PagedList<ReportSummary>> GetReportsAsync(bool? isApprove = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<ReportDetails>> GetByIdAsync(string id);
        Task ActiveAsync(string id, bool isApprove, int point = 0, int satisfiedPoint = 0, bool isDisableAccount = false);
        Task<PagedList<ReportSummary>> GetByUserIdAsync(string userId, bool? isApprove = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10);

    }
}
