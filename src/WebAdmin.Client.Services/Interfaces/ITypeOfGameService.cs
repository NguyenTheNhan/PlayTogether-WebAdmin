using System.Threading.Tasks;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ITypeOfGameService
    {
        Task<TypeOfGameSummary> CreateAsync(string gameTypeId, string gameId);
        Task<TypeOfGameSummary> GetByIdAsync(string id);

        Task DeleteAsync(string id);
    }
}
