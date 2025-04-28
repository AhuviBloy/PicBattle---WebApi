using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Creation")]
        public int CreationId { get; set; }

        public virtual Creation Creation { get; set; }

        [ForeignKey("Challenge")]
        public int ChallengeId { get; set; }

        public virtual Challenge Challenge { get; set; }
        public int UserId { get; set; } = 0;

        public string IpAddress { get; set; } = "";

        //[Range(1, 5)]
        public int Stars { get; set; }
        public DateTime RateAt { get; set; } = DateTime.UtcNow;
    }
}
