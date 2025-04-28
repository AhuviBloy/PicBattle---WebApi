using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public int CountCreations { get; set; } = 0;
        //public bool IsActive { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Creation> ChallengeCreationList { get; set; } = new List<Creation>();

    }
}


