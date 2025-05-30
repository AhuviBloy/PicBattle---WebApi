using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public enum ERole
    {
        Admin, User
    }
    public class User
    {
        [Key]
        public int Id { get; set; } 
        //public int UserId { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string PasswordHash { get; set; } 
        public ERole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public List<Creation> UserCreationList { get; set; } = new List<Creation>();

    }
}
