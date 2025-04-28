using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.DTOs
{
    public class CreationWithCreatorDTO
    {
        public int Id { get; set; }
        //public User User { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; } = 0;
        public string CreatorName { get; set; } = string.Empty;

    }
}
