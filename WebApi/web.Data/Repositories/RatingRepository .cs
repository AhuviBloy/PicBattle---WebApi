using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Repositories;

namespace web.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DataContext _context;

        public RatingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Rating> GetRatingByIpAndCreationAsync(string ipAddress, int creationId)
        {
            return await _context.Ratings.FirstOrDefaultAsync(r => r.IpAddress == ipAddress && r.CreationId == creationId);
        }


        public async Task<double> GetAverageRatingAsync(int creationId)
        {
            var ratings = await _context.Ratings.Where(r => r.CreationId == creationId).ToListAsync();
            return ratings.Count > 0 ? ratings.Average(r => r.Stars) : 0;
        }

        public async Task<Creation> GetWinnerCreationAsync(int challengeId)
        {
            // מניחים ש-DataContext כולל את הקשר ל-creations
            var winnerCreation = await _context.Ratings
                                               .Where(r => r.Creation.ChallengeId == challengeId)
                                               .GroupBy(r => r.CreationId)
                                               .OrderByDescending(g => g.Average(r => r.Stars))
                                               .FirstOrDefaultAsync();

            return winnerCreation?.FirstOrDefault()?.Creation; // מחזירים את היצירה עם הציון הגבוה ביותר
        }

        // קבלת כל ההצבעות
        public async Task<List<Rating>> GetAllRatingsAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

        // קבלת כל ההצבעות של משתמש מסוים
        public async Task<List<Rating>> GetUserRatingsAsync(int userId)
        {
            return await _context.Ratings.Where(r => r.UserId == userId).ToListAsync();
        }

        // הוספת הצבעה
        public async Task<bool> RateCreationAsync(Rating rating)
        {
            var creation= await _context.Creations.FirstOrDefaultAsync(c => c.Id == rating.CreationId);
            if (creation==null)
                return false;

            creation.Votes = creation.Votes + rating.Stars;
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return true;
        }

        // הסרת הצבעה
        public async Task<bool> RemoveRatingAsync(string ip, int creationId)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.IpAddress == ip && r.CreationId == creationId);
            if (rating == null) 
                return false;

            var creation = await _context.Creations.FirstOrDefaultAsync(c => c.Id == rating.CreationId);
            if (creation == null)
                return false;

            creation.Votes = creation.Votes - 1;

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Rating>> GetCreationsVotedByIpAsync(string ipAddress)
        {
            return await _context.Ratings.Where(r => r.IpAddress == ipAddress).ToListAsync();
        }

    }
}
