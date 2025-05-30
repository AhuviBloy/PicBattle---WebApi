using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace web.Data.Repositories
{

    public class CreationRepository : ICreationRepository
    {
        private readonly DataContext _context;

        public CreationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Creation>> GetAllCreationAsync()
        {
            return await _context.Creations.Where(c=>c.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Creation>> GetCreationsByChallengeAsync(int challengeId)
        {
            return await _context.Creations.Where(c => c.ChallengeId == challengeId && c.IsActive).ToListAsync();
        }

        public async Task<Creation> GetCreationByIdAsync(int id)
        {
            var creation = await _context.Creations.Where(c => c.Id == id && c.IsActive).FirstOrDefaultAsync(); ;
            return creation;
        }

        public async Task<bool> CreateCreationAsync(Creation creation)
        {
            var currentChallenge = await _context.Challenges.FirstOrDefaultAsync(c => c.Id == creation.ChallengeId);

            if (currentChallenge == null)
                return false;

            currentChallenge.CountCreations = currentChallenge.CountCreations + 1;

            await _context.Creations.AddAsync(creation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCreationAsync(int id, Creation creation)
        {
            var currentCreation = await GetCreationByIdAsync(id);
            if (currentCreation != null)
            {
                currentCreation.Votes = creation.Votes;
                currentCreation.ImageUrl = creation.ImageUrl;
                //currentCreation.UserId = creation.UserId;
                //currentCreation.ChallengeId = creation.ChallengeId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCreationVoteAsync(int id)
        {
            var currentCreation = await GetCreationByIdAsync(id);
            if (currentCreation == null)
                return false;
            currentCreation.Votes = currentCreation.Votes + 1;
            await _context.SaveChangesAsync();
            return true;
        }
        //public async Task<bool> DeleteCreationAsync(int id)
        //{
        //    var creation = await _context.Creations.FindAsync(id);
        //    if (creation == null) return false;

        //    //_context.Creations.Remove(creation);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}



        public async Task<bool> DeleteCreationAsync(int id)
        {
            var creation = await _context.Creations
                .Include(c => c.Challenge)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (creation == null) return false;

            // מחיקה רכה
            creation.IsActive = false;

            // עדכון מספר יצירות פעילות באתגר
            if (creation.Challenge != null)
            {
                var activeCreationsCount = await _context.Creations
                    .CountAsync(c => c.ChallengeId == creation.ChallengeId && c.IsActive);

                creation.Challenge.CountCreations = activeCreationsCount;
            }

            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<Creation> GetCreationWithUserAsync(int creationId)
        {
            return await _context.Creations
                .Include(c => c.User) // טעינת היוזר
                .FirstOrDefaultAsync(c => c.Id == creationId);
        }

        public async Task<List<Creation>> GetAllCreationsWithUserAsync(int challengeId)
        {
            return await _context.Creations.Where(c => c.ChallengeId == challengeId && c.IsActive).Include(c => c.User).ToListAsync();
        }

        public async Task<bool> UpdateDescriptionAsync(int id,string description)
        {
            var currentCreation = await GetCreationByIdAsync(id);
            if (currentCreation == null)
                return false;
            currentCreation.Description = description;
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
