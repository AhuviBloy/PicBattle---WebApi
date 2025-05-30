﻿using Microsoft.AspNetCore.Mvc;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Service;


namespace Web.Api.Controllers
{
    [Route("api/challenge")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengesController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetAllChallengesAsync()
        {
            var challenges = await _challengeService.GetAllChallengesAsync();
            return Ok(challenges);
        }
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetActiveChallengesAsync()
        {
            var challenges = await _challengeService.GetActiveChallengesAsync();
            return Ok(challenges);
        }

        [HttpGet("creations-for-challenge/{challengeId}")]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetCreationsForChallengeAsync(int challengeId)
        {
            var challenges = await _challengeService.GetCreationsForChallengeAsync(challengeId);
            return Ok(challenges);
        }

        [HttpGet("previous")]
        public async Task<ActionResult<IEnumerable<Challenge>>> GetPreviousChallengesAsync()
        {
            var challenges = await _challengeService.GetPreviousChallengesAsync();
            return Ok(challenges);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Challenge>> GetChallengeByIdAsync(int id)
        {
            var challenge = await _challengeService.GetChallengeByIdAsync(id);
            if (challenge == null)
                return NotFound($"Challenge with ID {id} not found.");
            return Ok(challenge);
        }

        // GET api/Challenge/winner/5
        [HttpGet("winner/{challengeId}")]
        public async Task<ActionResult<Creation>> GetWinCreationAsync(int challengeId)
        {
            var winner = await _challengeService.GetWinCreationAsync(challengeId);
            if (winner == null)
                return NotFound($"No winning creation found for Challenge ID {challengeId}.");
            return Ok(winner);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChallengeAsync([FromBody] ChallengePostDTO challenge)
        {
            var success = await _challengeService.CreateChallengeAsync(challenge);
            if (!success)
                return BadRequest("Failed to create challenge.");
            return Ok(success);
            //return CreatedAtAction(nameof(GetChallengeByIdAsync), new { id = challenge.Id }, challenge);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChallengeAsync(int id, [FromBody] Challenge challenge)
        {
            var success = await _challengeService.UpdateChallengeAsync(id, challenge);
            if (!success)
                return NotFound($"Challenge with ID {id} not found.");
            return NoContent();
        }

        //// PATCH api/Challenge/update-count/5
        //[HttpPatch("update-count/{id}")]
        //public async Task<ActionResult> UpdateCountCreationsAsync(int id)
        //{
        //    var success = await _challengeService.UpdateCountCreationsAsync(id);
        //    if (!success)
        //        return NotFound($"Challenge with ID {id} not found.");
        //    return Ok(new { message = "CountCreations updated successfully." });
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChallengeAsync(int id)
        {
            var success = await _challengeService.DeleteChallengeAsync(id);
            if (!success)
                return NotFound($"Challenge with ID {id} not found.");
            return NoContent();
        }
    }
}










