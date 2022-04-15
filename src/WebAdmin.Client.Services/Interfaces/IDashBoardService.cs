using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IDashBoardService
    {
        Task<ApiResponse<DashBoardResponse>> GetDashBoard();
    }
}
