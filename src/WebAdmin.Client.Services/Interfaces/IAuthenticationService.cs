using System.Threading.Tasks;
using WebAdmin.Shared.Models.Auth;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {

        Task<LoginResult> LoginUserAsync(LoginRequest model);



    }
}
