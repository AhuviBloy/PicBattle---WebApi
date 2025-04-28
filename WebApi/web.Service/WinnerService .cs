using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Repositories;
using web.Core.Services;

namespace web.Service
{
    public class WinnerService : IWinnerService
    {
        private readonly IWinnerRepository _winnerRepository;

        public WinnerService(IWinnerRepository winnerRepository)
        {
            _winnerRepository = winnerRepository;
        }

        // פונקציה 1: החזרת כל היצירות המנצחות
        public async Task<IEnumerable<WinCreation>> GetPreviousWinnersAsync()
        {
            return await _winnerRepository.GetPreviousWinnersAsync();
        }

        // פונקציה 2: החזרת יצירה מנצחת לאתגר מסוים
        public async Task<WinCreation> GetWinnerByChallengeAsync(int challengeId)
        {
            return await _winnerRepository.GetWinnerByChallengeAsync(challengeId);
        }

        // פונקציה 3: הוספת תמונה מנצחת
        public async Task<bool> AddWinningCreationAsync(WinCreation winCreation)
        {
            return await _winnerRepository.AddWinningCreationAsync(winCreation);
        }
    }
}
