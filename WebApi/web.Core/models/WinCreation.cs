using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public class WinCreation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Challenge")]
        public int ChallengeId { get; set; }
        public virtual Challenge Challenge { get; set; }

        [ForeignKey("Creation")]
        public int CreationId { get; set; }
        public virtual Creation Creation { get; set; }

        public DateTime WonAt { get; set; } = DateTime.UtcNow;
    }
}
