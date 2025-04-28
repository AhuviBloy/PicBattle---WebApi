using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Repositories;

namespace web.Data.Repositories
{
    public class WinnerRepository : IWinnerRepository
    {
        private readonly DataContext _context;

        public WinnerRepository(DataContext context)
        {
            _context = context;
        }

        // פונקציה 1: החזרת כל היצירות המנצחות
        public async Task<IEnumerable<WinCreation>> GetPreviousWinnersAsync()
        {
            return await _context.WinCreations.ToListAsync();
        }

        // פונקציה 2: החזרת יצירה מנצחת לאתגר מסוים
        public async Task<WinCreation> GetWinnerByChallengeAsync(int challengeId)
        {
            return await _context.WinCreations
                                 .FirstOrDefaultAsync(w => w.ChallengeId == challengeId);
        }

        // פונקציה 3: הוספת תמונה מנצחת
        public async Task<bool> AddWinningCreationAsync(WinCreation winCreation)
        {
            await _context.WinCreations.AddAsync(winCreation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
