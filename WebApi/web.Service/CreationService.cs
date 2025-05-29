using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Repositories;
using web.Core.Service;

namespace web.Service
{

    public class CreationService : ICreationService
    {
        private readonly ICreationRepository _creationRepository;
        private readonly IMapper _mapper;

        public CreationService(ICreationRepository creationRepository, IMapper mapper)
        {
            _creationRepository = creationRepository;
            _mapper = mapper;
        }

        public async Task<List<Creation>> GetAllCreationAsync()
        {
            return await _creationRepository.GetAllCreationAsync();
        }

        public async Task<IEnumerable<Creation>> GetCreationsByChallengeAsync(int challengeId)
        {
            return await _creationRepository.GetCreationsByChallengeAsync(challengeId);
        }

        public async Task<Creation> GetCreationByIdAsync(int id)
        {
            return await _creationRepository.GetCreationByIdAsync(id);
        }

        public async Task<bool> CreateCreationAsync(CreationPostDTO creation)
        {
            //var newCreation = new Creation
            //{
            //    FileName = creation.FileName,
            //    FileType = creation.FileType,
            //    UserId = creation.UserId,
            //    ChallengeId = creation.ChallengeId,
            //    ImageUrl = creation.ImageUrl
            //};
            var newCreation = _mapper.Map<Creation>(creation);
            return await _creationRepository.CreateCreationAsync(newCreation);
        }


        public async Task<bool> UpdateCreationAsync(int id, CreationPostDTO creation)
        {
            var tmp = _mapper.Map<Creation>(creation);
            return await _creationRepository.UpdateCreationAsync(id, tmp);
        }

        public async Task<bool> UpdateCreationVoteAsync(int id)
        {
            return await _creationRepository.UpdateCreationVoteAsync(id);
        }

        public async Task<bool> DeleteCreationAsync(int id)
        {
            return await _creationRepository.DeleteCreationAsync(id);
        }

        public async Task<string> GetCreatorNameAsync(int creationId)
        {
            var creation = await _creationRepository.GetCreationWithUserAsync(creationId);
            return creation?.User?.Name ?? "Unknown Creator";
        }

        public async Task<string> GetCreationDescriptionAsync(int creationId)
        {
            var creation = await _creationRepository.GetCreationWithUserAsync(creationId);
            return creation?.Description ?? "No Description";
        }
        public async Task<List<CreationWithCreatorDTO>> GetAllCreationsWithCreatorAsync(int challengeId)
        {
            var creations = await _creationRepository.GetAllCreationsWithUserAsync(challengeId);
            return creations.Select(c => new CreationWithCreatorDTO
            {
                Id = c.Id,
                FileName = c.FileName,
                FileType = c.FileType,
                UserId = c.UserId,
                ChallengeId = c.ChallengeId,
                ImageUrl = c.ImageUrl,
                Description = c.Description,
                Votes = c.Votes,
                CreatorName = c.User?.Name ?? "אנונימי"
            }).ToList();
        }

        public async Task<bool> UpdateDescriptionAsync(int id, string description)
        {
            return await _creationRepository.UpdateDescriptionAsync(id, description);
        }


    }

}
