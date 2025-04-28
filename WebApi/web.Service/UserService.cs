using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Repositories;
using web.Core.Service;

namespace web.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
        public async Task<bool> UpdateUserAsync(int id, UserUpdateDTO user)
        {
            var tmp = _mapper.Map<User>(user);
            return await _userRepository.UpdateUserAsync(id, tmp);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }




    //תמר
    //public class UserService:IUserService
    //{
    //    private readonly IUserRepository _userRepository;
    //    private readonly IMapper _mapper;
    //    private readonly IConfiguration _configuration;

    //    public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    //    {
    //        _userRepository = userRepository;
    //        _mapper = mapper;
    //        _configuration = configuration;
    //    }

    //    public async Task<List<User>> GetAllUsersAsync()
    //    {
    //        return await _userRepository.GetAllUsersAsync();
    //    }

    //    public async Task<User> GetUserByIdAsync(int id)
    //    {
    //        return await _userRepository.GetUserByIdAsync(id);
    //    }
    //    public async Task<User> GetUserByEmailAsync(string email)
    //    {
    //        return await _userRepository.GetUserByEmailAsync(email);
    //    }

    //    public async Task<bool> AddAdminAsync(UserPostDTO user)
    //    {
    //        var tmp = _mapper.Map<User>(user);
    //        return await _userRepository.AddAdminAsync(tmp);
    //    }

    //    public async Task<bool> AddUserAsync(UserPostDTO user)
    //    {
    //        var tmp = _mapper.Map<User>(user);
    //        return await _userRepository.AddUserAsync(tmp);
    //    }

    //    public async Task<bool> UpdateUserAsync(int id, UserUpdateDTO user)
    //    {
    //        var tmp = _mapper.Map<User>(user);
    //        return await _userRepository.UpdateUserAsync(id, tmp);
    //    }

    //    public async Task<bool> DeleteUserAsync(int id)
    //    {
    //        return await _userRepository.DeleteUserAsync(id);
    //    }

    //    public string GenerateJwtToken(int userId, string username, string email, string role)
    //    {
    //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //        var claims = new List<Claim>
    //        {
    //            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
    //            new Claim(ClaimTypes.Name, username),
    //            new Claim(ClaimTypes.Email, email),
    //            new Claim(ClaimTypes.Role, role) // הוספת תפקיד
    //        };
    //        var token = new JwtSecurityToken(
    //            issuer: _configuration["Jwt:Issuer"],
    //            audience: _configuration["Jwt:Audience"],
    //            claims: claims,
    //            expires: DateTime.Now.AddMinutes(30),
    //            signingCredentials: credentials
    //        );

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    //}
}
