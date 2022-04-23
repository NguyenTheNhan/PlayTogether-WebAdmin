using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.SystemConfig;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ISystemConfigService
    {
        Task<PagedList<SystemConfigDetail>> GetSystemConfigAsync(string query = null, int pageNumber = 1, int pageSize = 1000);
        Task<ApiResponse<SystemConfigDetail>> GetByIdAsync(string id);
        Task<ApiResponse<SystemConfigDetail>> GetByNoAsync(string no);

        Task<ApiResponse<SystemConfigDetail>> CreateAsync(SystemConfigDetail model);

        Task EditAsync(string id, float value);
        Task MultiUpdate(List<SystemConfigDetail> systemConfigs);

        Task DeleteAsync(string id);
    }
}
