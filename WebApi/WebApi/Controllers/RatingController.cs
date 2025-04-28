using Microsoft.AspNetCore.Mvc;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Services;

namespace Web.Api.Controllers
{
    [Route("api/rating")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("winner/{challengeId}")]
        public async Task<ActionResult<Creation>> GetWinnerCreationAsync(int challengeId)
        {
            var winnerCreation = await _ratingService.GetWinnerCreationAsync(challengeId);

            if (winnerCreation == null)
                return NotFound("No winner found for this challenge.");

            return Ok(winnerCreation);
        }

        // קבלת כל ההצבעות
        [HttpGet("all")]
        public async Task<ActionResult<List<Rating>>> GetAllRatingsAsync()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return Ok(ratings);
        }

        // קבלת ההצבעות של משתמש מסוים
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Rating>>> GetUserRatingsAsync(int userId)
        {
            var ratings = await _ratingService.GetUserRatingsAsync(userId);
            return Ok(ratings);
        }

        // הוספת הצבעה
        [HttpPost]
        public async Task<ActionResult> RateCreationAsync([FromBody] RatePostDTO rating)
        {
            var success = await _ratingService.RateCreationAsync(rating);
            if (!success)
                return BadRequest("Failed to rate creation.");

            return Ok("Creation rated successfully.");
        }

        // הסרת הצבעה
        [HttpDelete("{ip}/{creationId}")]
        public async Task<ActionResult> RemoveRatingAsync(string ip, int creationId)
        {
            var success = await _ratingService.RemoveRatingAsync(ip, creationId);
            if (!success)
                return NotFound("Rating not found.");

            return Ok("Rating removed successfully.");
        }

        [HttpGet("ip/{ipAddress}")]
        public async Task<ActionResult<List<Rating>>> GetCreationsVotedByIpAsync(string ipAddress)
        {
            var creationIds = await _ratingService.GetCreationsVotedByIpAsync(ipAddress);
            return Ok(creationIds);
        }

    }
}














