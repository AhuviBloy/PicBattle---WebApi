using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Service;
using web.Core.Services;
using web.Service;

namespace Web.Api.Controllers
{
    [Route("api/creation")]
    [ApiController]
    public class CreationsController : ControllerBase
    {
        private readonly ICreationService _creationService;
        private readonly IS3Service _s3Service;
        private readonly IAmazonS3 _s3Client;

        public CreationsController(ICreationService creationService, IS3Service s3Service, IAmazonS3 s3Client)
        {
            _creationService = creationService;
            _s3Service = s3Service;
            _s3Client = s3Client;
        }

        //קבלת כל היצירות לפי אתגר
        //[HttpGet("{challengeId}")]
        //public async Task<ActionResult<IEnumerable<Creation>>> GetCreationsByChallengeAsync(int challengeId)
        //{
        //    var creations = await _creationService.GetCreationsByChallengeAsync(challengeId);
        //    return Ok(creations);
        //}


        //⬆️ שלב 1: קבלת URL להעלאת קובץ ל-S3
        [HttpGet("upload-url")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetUploadUrl([FromQuery] string fileName, [FromQuery] string contentType)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("Missing file name");
            var url = await _s3Service.GeneratePresignedUrlAsync(fileName, contentType);
            return Ok(new { url });
        }

        // ⬇️ שלב 2: קבלת URL להורדת קובץ מה-S3
        [HttpGet("download-url/{fileName}")]
        public async Task<IActionResult> GetDownloadUrl(string fileName)
        {
            var url = await _s3Service.GetDownloadUrlAsync(fileName);
            return Ok(new { downloadUrl = url });
        }


        // GET: api/Creation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Creation>>> GetAllCreationsAsync()
        {
            var list = await _creationService.GetAllCreationAsync();
            if (list == null || list.Count == 0)
                return NotFound("No creations found.");
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Creation>> GetCreationByIdAsync(int id)
        {
            var creation = await _creationService.GetCreationByIdAsync(id);
            if (creation == null)
                return NotFound($"Creation with ID {id} not found.");
            return Ok(creation);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCreationAsync([FromBody] CreationPostDTO creation)
        {
            var success = await _creationService.CreateCreationAsync(creation);
            if (!success)
                return BadRequest("Failed to create creation.");

            return Ok(success);
        }

       // PUT api/Creation/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCreationAsync(int id, [FromBody] CreationPostDTO creation)
        {
            if (creation == null)
                return BadRequest("Invalid creation data.");

            var success = await _creationService.UpdateCreationAsync(id, creation);
            if (!success)
                return NotFound($"Creation with ID {id} not found.");
            return Ok("Creation updated successfully.");
        }

        // PATCH api/Creation/vote/5
        [HttpPatch("vote/{id}")]
        public async Task<ActionResult> UpdateCreationVoteAsync(int id)
        {
            var success = await _creationService.UpdateCreationVoteAsync(id);
            if (!success)
                return NotFound($"Creation with ID {id} not found.");
            return Ok("Vote count updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCreationAsync(int id)
        {
            var success = await _creationService.DeleteCreationAsync(id);
            if (!success)
                return NotFound($"Creation with ID {id} not found.");
            return NoContent();
        }

        [HttpGet("{creationId}/creator-name")]
        public async Task<IActionResult> GetCreatorName(int creationId)
        {
            var name = await _creationService.GetCreatorNameAsync(creationId);
            return Ok(name);
        }

        [HttpGet("{creationId}/description")]
        public async Task<IActionResult> GetCreationDescription(int creationId)
        {
            var description = await _creationService.GetCreationDescriptionAsync(creationId);
            return Ok(description);
        }

        [HttpGet("{challengeId}/with-creator")]
        public async Task<ActionResult<IEnumerable<CreationWithCreatorDTO>>> GetAllCreationsWithCreatorAsync(int challengeId)
        {
            var creations = await _creationService.GetAllCreationsWithCreatorAsync(challengeId);
            if (creations == null || creations.Count == 0)
                return NotFound("No creations found.");
            return Ok(creations);
        }

    }
}









