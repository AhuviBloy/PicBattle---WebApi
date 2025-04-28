using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.Repositories
{
    public interface IRatingRepository
    {
        Task<double> GetAverageRatingAsync(int creationId);
        Task<Rating> GetRatingByIpAndCreationAsync(string ipAddress, int creationId);
        Task<Creation> GetWinnerCreationAsync(int challengeId);
        Task<List<Rating>> GetAllRatingsAsync();
        Task<List<Rating>> GetUserRatingsAsync(int userId);
        Task<bool> RateCreationAsync(Rating rating);
        Task<bool> RemoveRatingAsync(string userId, int creationId);
        Task<List<Rating>> GetCreationsVotedByIpAsync(string ipAddress);

    }
}
