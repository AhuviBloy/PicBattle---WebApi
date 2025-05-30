using System.Threading.Tasks; 
using web.Core.DTOs;

namespace web.Core.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegisterDTO userDto); 
        Task<string?> LoginAsync(UserLoginDTO userDto);

    }
}
