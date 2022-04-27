using System;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Feedback;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<PagedList<FeedbackSummary>> GetFeedbacksAsync(string type = null, int isApprove = -1, bool GetAll = true, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<FeedbackDetail>> GetByIdAsync(string id);
        Task ActiveAsync(string id, int isApprove);
    }
}
