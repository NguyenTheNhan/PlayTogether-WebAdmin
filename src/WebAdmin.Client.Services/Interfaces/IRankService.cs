using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Rank;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IRankService
    {
        Task<RankDetail> CreateAsync(RankDetail model, string gameId);
        Task<RankDetail> EditAsync(string id, int no, string name, string gameId);
        Task<RankDetail> GetByIdAsync(string id);
        Task<PagedList<RankDetail>> GetRankAsync(string gameId);

        Task DeleteAsync(string id);
    }
}
