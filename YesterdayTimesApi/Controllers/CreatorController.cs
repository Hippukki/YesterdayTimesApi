using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Email;
using YesterdayTimesApi.Entities;
using YesterdayTimesApi.Pagination;

namespace YesterdayTimesApi.Controllers
{
    [Route("creators")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private readonly IRepository repository;

        private readonly IEmailService emailService;

        public CreatorController(IRepository repository, IEmailService emailService)
        {
            this.repository = repository;
            this.emailService = emailService;
        }
        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateCreatorAsync(CreatedCreator created)
        {
            Creator creator = new()
            {
                Id = Guid.NewGuid(),
                fullName = created.fullName,
                Password = created.Password,
                Role = "admin"
            };
            await repository.CreateCreatorAsync(creator);
            return CreatedAtAction(nameof(GetCreatorAsync), new { creator.Id }, creator.CreatorAsDetailedDTO());
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<CreatorDetailedDTO>> GetCreatorAsync(Guid id)
        {
            var creator = await repository.GetCreatorAsync(id);
            if (creator is null)
            {
                return NotFound();
            }
            return creator.CreatorAsDetailedDTO();
        }
        [Authorize(Roles = "admin, user")]
        [HttpGet("get")]
        public async Task<IEnumerable<CreatorDetailedDTO>> GetCreatorsAsync([FromQuery] CreatorQueryParameters parameters)
        {
            var creators = (await repository.GetCreatorsAsync(parameters)).Select(creator => creator.CreatorAsDetailedDTO());
            Response.Headers.Add("X-Total-Count", creators.Count().ToString());
            return creators;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCreatorAsync(Guid id)
        {
            var existingCreator = await repository.GetCreatorAsync(id);
            if (existingCreator is null)
            {
                return NotFound();
            }
            await repository.DeleteCreatorAsync(id);
            return NoContent();
        }
    }
}
