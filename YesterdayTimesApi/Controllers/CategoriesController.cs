using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;

namespace YesterdayTimesApi.Controllers 
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository repository;

        public CategoriesController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategoryAsync(CreatedCategory created)
        {
            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = created.Name,
            };
            await repository.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryAsync), new { Id = category.Id }, category.CategoryAsDTO());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryAsync(Guid id)
        {
            var category = await repository.GetCategoryAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            return category.CategoryAsDTO();
        }
    }
}
