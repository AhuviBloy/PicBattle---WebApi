using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;

namespace web.Core.Service
{
    public interface IChallengeService
    {
        Task<IEnumerable<Challenge>> GetAllChallengesAsync();
        Task<IEnumerable<Creation>> GetCreationsForChallengeAsync(int challengeId);

        Task<IEnumerable<Challenge>> GetActiveChallengesAsync();
        Task<IEnumerable<Challenge>> GetPreviousChallengesAsync();
        Task<Challenge> GetChallengeByIdAsync(int id);
        Task<Creation> GetWinCreationAsync(int challengeId);

        Task<bool> CreateChallengeAsync(ChallengePostDTO challenge);
        Task<bool> UpdateChallengeAsync(int id, Challenge challenge);
        Task<bool> DeleteChallengeAsync(int id);
    }


    //public interface IChallengeService
    //{
    //    Task<List<Challenge>> GetAllChallengesAsync();
    //    Task<Challenge> GetChallengeByIdAsync(int id);
    //    Task<List<Challenge>> GetSortChallengeAsync();
    //    Task<List<Creation>> GetCreationsByChallengeAsync(int challengeId);
    //    Task<Creation> GetWinCreationAsync(int challengeId);
    //    Task<bool> AddChallengeAsync(ChallengePostDTO challenge);
    //    Task<bool> UpdateChallengeAsync(int id, ChallengePostDTO challenge);
    //    Task<bool> UpdateCountCreationsAsync(int id);
    //    Task<bool> DeleteChallengeAsync(int id);
    //}
}
