using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Game;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IGameService
    {
        Task<PagedList<GameSummary>> GetGamesAsync(string query = null, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<GameDetail>> GetByIdAsync(string id);

        Task<ApiResponse<GameDetail>> CreateAsync(GameDetail model);

        Task<ApiResponse<GameDetail>> EditAsync(GameDetail model);

        Task DeleteAsync(string id);
    }
}
