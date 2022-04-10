using System;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Report;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IReportService
    {
        Task<PagedList<ReportSummary>> GetReportsAsync(bool? isApprove = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10);
        Task<ReportDetails> GetByIdAsync(string id);
        Task ActiveAsync(string id, bool isApprove);
        Task<PagedList<ReportSummary>> GetByUserIdAsync(string userId, bool? isApprove = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10);

    }
}
