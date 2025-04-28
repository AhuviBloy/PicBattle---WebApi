using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;

namespace web.Core.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(int id, UserUpdateDTO user);

        Task<bool> DeleteUserAsync(int id);
    }

    //תמר
    //public interface IUserService
    //{
    //    Task<List<User>> GetAllUsersAsync();
    //    Task<User> GetUserByIdAsync(int id);
    //    Task<User> GetUserByEmailAsync(string email);
    //    Task<bool> AddAdminAsync(UserPostDTO user);
    //    Task<bool> AddUserAsync(UserPostDTO user);
    //    Task<bool> UpdateUserAsync(int id, UserUpdateDTO user);
    //    Task<bool> DeleteUserAsync(int id);
    //    string GenerateJwtToken(int userId, string username, string email, string role);
    //}
}
