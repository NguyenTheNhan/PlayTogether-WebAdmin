using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Rank;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IRankService
    {
        Task<RankDetail> CreateAsync(int no, string name, string gameId);
        Task<RankDetail> EditAsync(string id, int no, string name, string gameId);
        Task<RankDetail> GetByIdAsync(string id);
        Task<IEnumerable<RankDetail>> GetRankAsync(string gameId, int pageNumber = 1, int pageSize = 10);

        Task DeleteAsync(string id);
    }
}
