using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Repositories
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user);
        Task<User> LoginAsync(string email, string password);
        Task<User> GetProfileAsync(int userId);
    }
}
