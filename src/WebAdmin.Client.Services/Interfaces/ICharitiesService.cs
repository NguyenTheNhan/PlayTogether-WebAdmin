using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Charities;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ICharitiesService
    {
        Task<PagedList<CharitiesSummary>> GetCharitiesAsync(string query = null, bool isActive = true, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<CharitiesSummary>> GetByIdAsync(string id);

        Task CreateAsync(CharityCreate model);

        Task EditAsync(CharitiesSummary model);

        Task ActiveAsync(string id, bool isActive);

    }
}
