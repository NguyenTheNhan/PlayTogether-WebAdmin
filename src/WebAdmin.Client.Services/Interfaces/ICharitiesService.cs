using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Charities;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ICharitiesService
    {
        Task<IEnumerable<CharitiesSummary>> GetCharitiessAsync(string query = null, int pageNumber = 1, int pageSize = 10);
        Task<CharitiesSummary> GetByIdAsync(string id);

        Task<CharitiesSummary> CreateAsync(CharitiesSummary model);

        Task<CharitiesSummary> EditAsync(CharitiesSummary model);

        Task DeleteAsync(string id);
    }
}
