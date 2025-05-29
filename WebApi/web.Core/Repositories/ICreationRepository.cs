using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Repositories
{

    public interface ICreationRepository
    {
        Task<List<Creation>> GetAllCreationAsync();
        Task<IEnumerable<Creation>> GetCreationsByChallengeAsync(int challengeId);
        Task<Creation> GetCreationByIdAsync(int id);
        Task<bool> CreateCreationAsync(Creation creation);
        Task<bool> UpdateCreationAsync(int id, Creation creation);
        Task<bool> UpdateCreationVoteAsync(int id);
        Task<bool> DeleteCreationAsync(int id);
        Task<Creation> GetCreationWithUserAsync(int creationId);
        Task<List<Creation>> GetAllCreationsWithUserAsync(int challengeId);
        Task<bool> UpdateDescriptionAsync(int id, string description);

    }

    //public interface ICreationRepository
    //{
    //    Task<List<Creation>> GetAllCreationAsync();
    //    Task<Creation> GetCreationByIdAsync(int id);
    //    Task<bool> AddCreationAsync(Creation creation);
    //    Task<bool> UpdateCreationAsync(int id, Creation creation);
    //    Task<bool> UpdateCreationVoteAsync(int id);
    //    Task<bool> DeleteCreationAsync(int id);
    //}
}
