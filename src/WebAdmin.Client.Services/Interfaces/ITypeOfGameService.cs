using System.Threading.Tasks;
using WebAdmin.Shared.Models.TypeOfGame;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ITypeOfGameService
    {
        Task<ApiResponse<TypeOfGameSummary>> CreateAsync(string gameTypeId, string gameId);
        Task<ApiResponse<TypeOfGameSummary>> GetByIdAsync(string id);

        Task DeleteAsync(string id);
    }
}
