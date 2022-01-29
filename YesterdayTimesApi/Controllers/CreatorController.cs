using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;

namespace YesterdayTimesApi.Controllers
{
    [Route("creators")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private readonly IRepository repository;

        public CreatorController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateCreatorAsync(CreatedCreator created)
        {
            Creator creator = new()
            {
                Id = Guid.NewGuid(),
                fullName = created.fullName,
            };
            await repository.CreateCreatorAsync(creator);
            return NoContent();
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<CreatorDTO>> GetCreatorAsync(Guid id)
        {
            var creator = await repository.GetCreatorAsync(id);
            if (creator is null)
            {
                return NotFound();
            }
            return creator.CreatorAsDTO();
        }
        [HttpGet("get")]
        public async Task<IEnumerable<CreatorDTO>> GetCreatorsAsync()
        {
            var creators = (await repository.GetCreatorsAsync()).Select(creator => creator.CreatorAsDTO());
            return creators;
        }
    }
}
