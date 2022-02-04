using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;


namespace YesterdayTimesApi.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IRepository repository;

        public ArticlesController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateArticleAsync(CreatedArticle created, Guid idCreator)
        {
            var creator = await repository.GetCreatorAsync(idCreator);
            var category = await repository.GetCategoryAsync(created.idCategory);
            if(category is null)
            {
                return NotFound("Selected category doesn not exist");
            }
            Article article = new()
            {
                Id = Guid.NewGuid(),
                Title = created.Title,
                Body = created.Body,
                createdDate = DateTimeOffset.UtcNow,
                Creators = new() { creator },
                CategoryID = created.idCategory,
                Category = category
            };
            creator.Articles = new() { article };
            await repository.CreateArticleAsync(article);
            await repository.UpdateCreatorAsync();
            return NoContent();
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ArticleDetailedDTO>> GetArticleAsync(Guid id)
        {
            var article = await repository.GetArticleAsync(id);
            if (article is null)
            {
                return NotFound();
            }
            return article.ArticleAsDetailedDTO();
        }
        [HttpGet("get")]
        public async Task<IEnumerable<ArticleDetailedDTO>> GetArticlesAsync()
        {
            var articles = (await repository.GetArticlesAsync())
                .Select(article => article.ArticleAsDetailedDTO());
            return articles;
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateArticleAsync(Guid id, UpdatedArticle updated)
        {
            var existingArticle = await repository.GetArticleAsync(id);
            if (existingArticle is null)
            {
                return NotFound();
            }
            existingArticle.Title = updated.Title;
            existingArticle.Body = updated.Body;
            await repository.UpdateArticleAsync();
            return NoContent();
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteArticleAsync(Guid id)
        {
            var existingArticle = await repository.GetArticleAsync(id);
            if (existingArticle is null)
            {
                return NotFound();
            }
            await repository.DeleteArticleAsync(id);
            return NoContent();
        }
    }
}
