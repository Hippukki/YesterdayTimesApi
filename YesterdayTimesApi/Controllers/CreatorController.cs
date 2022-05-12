using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;

namespace YesterdayTimesApi.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("creators")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private readonly IRepository repository;

        public CreatorController(IRepository repository)
        {
            this.repository = repository;
        }
        //[AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult> CreateCreatorAsync(CreatedCreator created)
        {
            Creator creator = new()
            {
                Id = Guid.NewGuid(),
                fullName = created.fullName,
                Role = created.Role
            };
            await repository.CreateCreatorAsync(creator);
            return CreatedAtAction(nameof(GetCreatorAsync), new { Id = creator.Id }, creator.CreatorAsDetailedDTO());
        }
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
        [HttpGet("get")]
        public async Task<IEnumerable<CreatorDetailedDTO>> GetCreatorsAsync()
        {
            var creators = (await repository.GetCreatorsAsync()).Select(creator => creator.CreatorAsDetailedDTO());
            return creators;
        }
    }
}
