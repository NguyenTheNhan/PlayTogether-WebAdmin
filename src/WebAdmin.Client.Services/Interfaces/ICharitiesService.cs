using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.Charities;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ICharitiesService
    {
        Task<IEnumerable<CharitiesSummary>> GetCharitiesAsync(string query = null, bool isActive = true, int pageNumber = 1, int pageSize = 10);
        Task<CharitiesSummary> GetByIdAsync(string id);

        Task CreateAsync(CharityCreate model);

        Task<CharitiesSummary> EditAsync(CharitiesSummary model);

        Task<CharitiesSummary> ActiveAsync(string id, bool isActive);

    }
}
