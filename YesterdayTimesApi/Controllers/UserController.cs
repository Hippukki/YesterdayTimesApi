using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;
using YesterdayTimesApi.Pagination;

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
                Role = "user",
                Categories = selectedCategories                
            };
            await repository.RegistrateUserAsync(user);
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpGet("users")]// For testing, delete in prod
        public async Task<IEnumerable<UserDetailedDTO>> GetUsersAsync([FromQuery] UserQueryParameters parameters)
        {
            var users = (await repository.GetUsersAsync(parameters)).Select(user => user.UserAsDetailedDTO());
            Response.Headers.Add("X-Total-Count", users.Count().ToString());
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
