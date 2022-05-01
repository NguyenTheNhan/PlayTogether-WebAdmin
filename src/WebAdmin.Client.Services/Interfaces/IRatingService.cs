using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Rating;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IRatingService
    {
        Task<PagedList<RatingDetail>> GetRatingsAsync(bool? isApprove = null, bool? isViolate = null, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<RatingDetail>> GetByIdAsync(string id);
        Task ActiveAsync(string id, bool isApprove);
    }
}
