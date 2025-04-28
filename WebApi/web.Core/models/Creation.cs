using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public class Creation
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public string FileName { get; set; } 
        public string FileType { get; set; } 
        public int UserId { get; set; } 
        public int ChallengeId { get; set; } 
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public bool IsActive { get; set; } = false;
       
    }
}
