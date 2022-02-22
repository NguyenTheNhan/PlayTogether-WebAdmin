using System.Threading.Tasks;
using WebAdmin.Shared.Models;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {

        Task<LoginResult> LoginUserAsync(LoginRequest model);

    }
}
