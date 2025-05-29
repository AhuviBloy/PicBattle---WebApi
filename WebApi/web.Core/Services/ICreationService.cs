using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;

namespace web.Core.Service
{

    public interface ICreationService
    {
        Task<List<Creation>> GetAllCreationAsync();
        Task<IEnumerable<Creation>> GetCreationsByChallengeAsync(int challengeId);
        Task<Creation> GetCreationByIdAsync(int id);
        Task<bool> CreateCreationAsync(CreationPostDTO creation);
        Task<bool> UpdateCreationAsync(int id, CreationPostDTO creation);
        Task<bool> UpdateCreationVoteAsync(int id);
        Task<bool> DeleteCreationAsync(int id);
        Task<string> GetCreatorNameAsync(int creationId);
        Task<string> GetCreationDescriptionAsync(int creationId);
        Task<List<CreationWithCreatorDTO>> GetAllCreationsWithCreatorAsync(int challengeId);
        Task<bool> UpdateDescriptionAsync(int id, string description);


    }

}
