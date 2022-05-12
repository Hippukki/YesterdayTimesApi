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
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository repository;

        public CategoriesController(IRepository repository)
        {
            this.repository = repository;
        }
        //[Authorize(Roles ="admin")]
        [HttpPost("create")]
        public async Task<ActionResult<CategoryDetailedDTO>> CreateCategoryAsync(CreatedCategory created)
        {
            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = created.Name,
            };
            await repository.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryAsync), new { Id = category.Id }, category.CategoryAsDetailedDTO());
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<CategoryDetailedDTO>> GetCategoryAsync(Guid id)
        {
            var category = await repository.GetCategoryAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            return category.CategoryAsDetailedDTO();
        }
        [HttpGet("get")]
        public async Task<IEnumerable<CategoryDetailedDTO>> GetCategoriesAsync()
        {
            var categories = (await repository.GetCategoriesAsync()).Select(category => category.CategoryAsDetailedDTO());
            return categories;
        }
        //[Authorize(Roles ="admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await repository.GetCategoryAsync(id);
            if (existingCategory is null)
            {
                return NotFound();
            }
            await repository.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
