using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Game;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameSummary>> GetGamesAsync(string query = null, int pageNumber = 1, int pageSize = 10);
        Task<GameDetail> GetByIdAsync(string id);

        Task<GameDetail> CreateAsync(GameDetail model);

        Task<GameDetail> EditAsync(GameDetail model);

        Task DeleteAsync(string id);
    }
}
