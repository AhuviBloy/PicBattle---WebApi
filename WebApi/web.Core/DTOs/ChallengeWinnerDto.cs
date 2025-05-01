using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.DTOs
{
    public class ChallengeWinnerDto
    {
        public int ChallengeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }

        public int? WinningCreationId { get; set; }
        public string? WinningImageUrl { get; set; }
        public int Votes { get; set; }
        public string? WinnerUserName { get; set; }
    }
}
