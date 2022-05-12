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
    [Authorize(Roles = "user, admin")]
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository repository;

        public UserController(IRepository repository)
        {
            this.repository = repository;
        }
        [AllowAnonymous]
        [HttpPost("singup")]
        public async Task<ActionResult> RegisterUserAsync(CreatedUser created)
        {
            List<Category> selectedCategories = new()
            {
                await repository.GetCategoryAsync(created.SelctedCategoryId1),
                await repository.GetCategoryAsync(created.SelctedCategoryId2),
                await repository.GetCategoryAsync(created.SelctedCategoryId3)
            };

            User user = new()
            {
                Id = Guid.NewGuid(),
                Email = created.Email,
                Password = created.Password,
                Role = created.Role,
                Categories = selectedCategories                
            };
            await repository.RegistrateUserAsync(user);
            return NoContent();
        }
        [HttpGet("users")]// For testing, delete in prod
        public async Task<IEnumerable<UserDetailedDTO>> GetAsync()
        {
            var users = (await repository.GetUsersAsync()).Select(user => user.UserAsDetailedDTO());
            return users;
        }
        [HttpGet("articles")]
        public async Task<IEnumerable<ArticleDTO>> GetUsersArticlesAsync(Guid id)
        {
            var user = await repository.GetUserAsync(id);
            List<ArticleDTO> articles = new();
            foreach(Category category in user.Categories)
            {
                var _category = await repository.GetCategoryAsync(category.Id);
                foreach (Article article in _category.Articles)
                {
                    articles.Add(article.ArticleAsDTO());
                }
            }
            return articles;
        }
    }
}
