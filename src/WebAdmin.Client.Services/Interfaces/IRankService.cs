using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Rank;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IRankService
    {
        Task<ApiResponse<RankDetail>> CreateAsync(int no, string name, string gameId);
        Task<ApiResponse<RankDetail>> EditAsync(string id, int no, string name, string gameId);
        Task<ApiResponse<RankDetail>> GetByIdAsync(string id);
        Task<PagedList<RankDetail>> GetRankAsync(string gameId, int pageNumber = 1, int pageSize = 10);

        Task DeleteAsync(string id);
    }
}
