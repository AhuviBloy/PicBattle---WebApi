using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Services
{
    public interface IWinnerService
    {
        Task<bool> AddWinningCreationAsync(WinCreation winCreation);
        Task<IEnumerable<WinCreation>> GetPreviousWinnersAsync();
        Task<WinCreation> GetWinnerByChallengeAsync(int challengeId);


    }
}
