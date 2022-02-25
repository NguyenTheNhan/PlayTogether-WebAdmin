using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.GameType;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IGameTypeService 
    {
        //
        Task<IEnumerable<GameTypeSummary>> GetGameTypesAsync(string query = null, int pageNumber = 1, int pageSize = 10);
        Task<GameTypeSummary> GetByIdAsync(string id);

        Task<GameTypeSummary> CreateAsync(GameTypeSummary model);

        Task<GameTypeSummary> EditAsync(GameTypeSummary model, string id);
        //Task<GameTypeSummary> EditAsync( string id, string name, string shortName, string otherName, string description);

        Task DeleteAsync(string id);
    }
}
