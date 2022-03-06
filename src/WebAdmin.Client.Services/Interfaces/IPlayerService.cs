using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Player;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerSummary>> GetPlayersAsync(string query = null, string status = null, bool? isActive = null, int pageNumber = 1, int pageSize = 10);
        Task<PlayerSummary> GetByIdAsync(string id);

        Task<PlayerSummary> ActiveAsync(string id, bool isActive, string message);
    }
}
