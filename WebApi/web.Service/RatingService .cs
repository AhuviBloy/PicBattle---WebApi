using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Repositories;
using web.Core.Services;

namespace web.Service
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }


        public async Task<Rating> GetRatingByIpAndCreationAsync(string ipAddress, int creationId)
        {
            return await _ratingRepository.GetRatingByIpAndCreationAsync(ipAddress, creationId);
        }


        public async Task<double> GetAverageRatingAsync(int creationId)
        {
            return await _ratingRepository.GetAverageRatingAsync(creationId);
        }

        public async Task<Creation> GetWinnerCreationAsync(int challengeId)
        {
            return await _ratingRepository.GetWinnerCreationAsync(challengeId);
        }

        public async Task<List<Rating>> GetAllRatingsAsync()
        {
            return await _ratingRepository.GetAllRatingsAsync();
        }

        public async Task<List<Rating>> GetUserRatingsAsync(int userId)
        {
            return await _ratingRepository.GetUserRatingsAsync(userId);
        }

        public async Task<bool> RateCreationAsync(RatePostDTO rating)
        {
            var newRate = new Rating
            {
                CreationId = rating.CreationId,
                ChallengeId = rating.ChallengeId,
                UserId = rating.UserId,
                IpAddress = rating.IpAddress,
                Stars = rating.Stars

            };
            return await _ratingRepository.RateCreationAsync(newRate);
        }

        public async Task<bool> RemoveRatingAsync(string ip, int creationId)
        {
            return await _ratingRepository.RemoveRatingAsync(ip, creationId);
        }
        public async Task<List<Rating>> GetCreationsVotedByIpAsync(string ipAddress)
        {
            return await _ratingRepository.GetCreationsVotedByIpAsync(ipAddress);
        }


    }
}
