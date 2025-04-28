using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Repositories
{
    public interface IChallengeRepository
    {
        Task<IEnumerable<Challenge>> GetAllChallengesAsync();
        Task<IEnumerable<Creation>> GetCreationsForChallengeAsync(int challengeId);
        Task<IEnumerable<Challenge>> GetActiveChallengesAsync();
        Task<IEnumerable<Challenge>> GetPreviousChallengesAsync();
        Task<Creation> GetWinCreationAsync(int challengeId);
        Task<Challenge?> GetChallengeByIdAsync(int id);
        Task<bool> CreateChallengeAsync(Challenge challenge);
        Task<bool> UpdateChallengeAsync(int id, Challenge challenge);
        Task<bool> DeleteChallengeAsync(int id);
    }




    //public interface IChallengeRepository
    //{
    //    Task<List<Challenge>> GetAllChallengesAsync();
    //    Task<Challenge> GetChallengeByIdAsync(int id);
    //    Task<List<Challenge>> GetSortChallengeAsync();
    //    Task<List<Creation>> GetSortVotesCreationsByChallengeAsync(int challengeId);
    //    Task<Creation> GetWinCreationAsync(int ChallengeId);
    //    Task<bool> AddChallengeAsync(Challenge challenge);
    //    Task<bool> UpdateChallengeAsync(int id, Challenge challenge);
    //    Task<bool> UpdateCountCreationsAsync(int id);
    //    Task<bool> DeleteChallengeAsync(int id);
    //}
}
