using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace web.Data.Repositories
{


    public class ChallengeRepository : IChallengeRepository
    {
        private readonly DataContext _context;

        public ChallengeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Challenge>> GetAllChallengesAsync()
        {
            return await _context.Challenges.ToListAsync();
        }
        public async Task<IEnumerable<Creation>> GetCreationsForChallengeAsync(int challengeId)
        {

            var challenge = await GetChallengeByIdAsync(challengeId);

            return challenge?.ChallengeCreationList ?? new List<Creation>();

        }
        public async Task<IEnumerable<Challenge>> GetActiveChallengesAsync()
        {
            return await _context.Challenges
                .Where(c => c.StartDate <= DateTime.Now && c.EndDate > DateTime.Now)
                .OrderBy(c => c.EndDate)
                .ToListAsync();
        }


        public async Task<IEnumerable<Challenge>> GetPreviousChallengesAsync()
        {
            return await _context.Challenges
                .Where(c => c.EndDate <= DateTime.Now)
                .OrderByDescending(c => c.EndDate)
                .ToListAsync();
        }


        public async Task<Challenge?> GetChallengeByIdAsync(int id)
        {
            return await _context.Challenges.AsNoTracking().Include(i => i.ChallengeCreationList)
              .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> CreateChallengeAsync(Challenge challenge)
        {
            await _context.Challenges.AddAsync(challenge);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Creation?> GetWinCreationAsync(int challengeId)
        {
            return await _context.Creations
                .Include(c => c.User)
                .Where(c => c.ChallengeId == challengeId)
                .OrderByDescending(c => c.Votes)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> UpdateChallengeAsync(int id, Challenge challenge)
        {
            _context.Entry(challenge).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteChallengeAsync(int id)
        {
            var challenge = await _context.Challenges.FindAsync(id);
            if (challenge == null) return false;

            _context.Challenges.Remove(challenge);
            await _context.SaveChangesAsync();
            return true;
        }

    }


    //public class ChallengeRepository : IChallengeRepository
    //{
    //    private readonly DataContext _context;
    //    public ChallengeRepository(DataContext context)
    //    {
    //        _context = context;
    //    }
    //    public async Task<List<Challenge>> GetAllChallengesAsync()
    //    {
    //        return await _context.ChallengeList.Include(c => c.ChallengeCreationList).ToListAsync();
    //    }

    //    public async Task<Challenge> GetChallengeByIdAsync(int id)
    //    {
    //        return await _context.ChallengeList.Include(c => c.ChallengeCreationList).FirstOrDefaultAsync(challenge => challenge.Id == id);
    //    }

    //    public async Task<List<Creation>> GetSortVotesCreationsByChallengeAsync(int challengeId)
    //    {
    //        var challenge = await GetChallengeByIdAsync(challengeId);
    //        var creationList = challenge.ChallengeCreationList.OrderByDescending(c => c.Votes).ToList();
    //        return creationList;
    //    }
    //    public async Task<List<Challenge>> GetSortChallengeAsync()
    //    {
    //        var challenges = await _context.ChallengeList
    //                        .OrderByDescending(c => c.CreatedAt)
    //                        .ToListAsync();
    //        return challenges;
    //    }

    //    public async Task<Creation> GetWinCreationAsync(int challengeId)
    //    {
    //        var creationList = await GetSortVotesCreationsByChallengeAsync(challengeId);
    //        return creationList.FirstOrDefault();
    //    }

    //    public async Task<bool> AddChallengeAsync(Challenge challenge)
    //    {
    //        if (await _context.ChallengeList.AnyAsync(c => c.Id == challenge.Id))
    //            return false;
    //        await _context.ChallengeList.AddAsync(challenge);
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }

    //    public async Task<bool> UpdateChallengeAsync(int id, Challenge challenge)
    //    {
    //        var currentChallenge = await GetChallengeByIdAsync(id);
    //        if (currentChallenge != null)
    //        {
    //            currentChallenge.Title = challenge.Title;
    //            currentChallenge.Description = challenge.Description;
    //            currentChallenge.CountCreations = challenge.CountCreations;

    //            await _context.SaveChangesAsync();
    //            return true;
    //        }
    //        return false;
    //    }
    //    public async Task<bool> UpdateCountCreationsAsync(int id)
    //    {
    //        var currentChallenge = await GetChallengeByIdAsync(id);
    //        if (currentChallenge == null)
    //            return false;
    //        currentChallenge.CountCreations = currentChallenge.CountCreations + 1;
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }

    //    public async Task<bool> DeleteChallengeAsync(int id)
    //    {
    //        var challenge = await GetChallengeByIdAsync(id);
    //        if (challenge == null)
    //            return false;
    //        challenge.IsActive = true;
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }

    //}
}
