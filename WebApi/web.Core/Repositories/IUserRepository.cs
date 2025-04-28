using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
         Task<User> GetUserByIdAsync(int id);
         Task<User> GetUserByEmailAsync(string email);
         Task<bool> UpdateUserAsync(int id, User user);

        Task<bool> DeleteUserAsync(int id);
    }

    //תמר
    //public interface IUserRepository
    //{
    //    Task<List<User>> GetAllUsersAsync();
    //    Task<User> GetUserByIdAsync(int id);
    //    Task<User> GetUserByEmailAsync(string email);
    //    Task<bool> AddAdminAsync(User user);
    //    Task<bool> AddUserAsync(User user);
    //    Task<bool> UpdateUserAsync(int id, User user);
    //    Task<bool> DeleteUserAsync(int id);
    //}
}
