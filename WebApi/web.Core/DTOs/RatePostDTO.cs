using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.DTOs
{
    public class RatePostDTO
    {
        public int CreationId { get; set; }

        public int ChallengeId { get; set; }

        public int UserId { get; set; }

        public string IpAddress { get; set; } 

        public int Stars { get; set; }
    }
}
