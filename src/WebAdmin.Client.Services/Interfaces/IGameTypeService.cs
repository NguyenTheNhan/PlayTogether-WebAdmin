using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.GameType;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IGameTypeService
    {
        //
        Task<PagedList<GameTypeSummary>> GetGameTypesAsync(string query = null, int pageNumber = 1, int pageSize = 1000);
        Task<ApiResponse<GameTypeSummary>> GetByIdAsync(string id);

        Task<ApiResponse<GameTypeSummary>> CreateAsync(GameTypeSummary model);

        Task<ApiResponse<GameTypeSummary>> EditAsync(GameTypeSummary model);

        Task DeleteAsync(string id);
    }
}
