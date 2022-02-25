using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Shared.Models.TypeOfGame;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ITypeOfGameService
    {
        Task<TypeOfGameDetail> CreateAsync(string gameTypeId, string gameId);
        Task<TypeOfGameDetail> GetByIdAsync(string id);

        Task DeleteAsync(string id);
    }
}
