using Microsoft.AspNetCore.Mvc;
using web.Core.models;
using web.Core.Services;

namespace Web.Api.Controllers
{
    [Route("api/winners")]
    [ApiController]
    public class WinnersController : ControllerBase
    {
        private readonly IWinnerService _winnerService;

        public WinnersController(IWinnerService winnerService)
        {
            _winnerService = winnerService;
        }

        // פעולה 1: החזרת כל היצירות המנצחות
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WinCreation>>> GetPreviousWinnersAsync()
        {
            var winners = await _winnerService.GetPreviousWinnersAsync();
            return Ok(winners);
        }

        // פעולה 2: החזרת יצירה מנצחת לאתגר מסוים
        [HttpGet("challenge/{challengeId}")]
        public async Task<ActionResult<WinCreation>> GetWinnerByChallengeAsync(int challengeId)
        {
            var winner = await _winnerService.GetWinnerByChallengeAsync(challengeId);
            if (winner == null)
                return NotFound("No winner found for this challenge.");
            return Ok(winner);
        }

        // פעולה 3: הוספת תמונה מנצחת
        [HttpPost]
        public async Task<ActionResult> AddWinningCreationAsync([FromBody] WinCreation winCreation)
        {
            var success = await _winnerService.AddWinningCreationAsync(winCreation);
            if (!success)
                return BadRequest("Failed to add winning creation.");
            return Ok("Winning creation added successfully.");
        }
    }
}
