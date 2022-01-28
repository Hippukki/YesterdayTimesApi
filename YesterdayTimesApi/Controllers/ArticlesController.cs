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

        [HttpPost]
        public async Task<ActionResult<ArticleDTO>> CreateArticleAsync(CreatedArticle created)
        {
            Article article = new()
            {
                Id = Guid.NewGuid(),
                Title = created.Title,
                Body = created.Body,
                createdDate = DateTimeOffset.UtcNow
            };
            await repository.CreateArticleAsync(article);
            return CreatedAtAction(nameof(GetArticleAsync), new { Id = article.Id }, article.ArticleAsDTO());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> GetArticleAsync(Guid id)
        {
            var article = await repository.GetArticleAsync(id);
            if (article is null)
            {
                return NotFound();
            }
            return article.ArticleAsDTO();
        }
        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> GetArticlesAsync()
        {
            var articles = (await repository.GetArticlesAsync()).Select(article => article.ArticleAsDTO());
            return articles;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArticleAsync(Guid id, UpdatedArticle updated)
        {
            var existingArticle = await repository.GetArticleAsync(id);
            if (existingArticle is null)
            {
                return NotFound();
            }
            Article article = existingArticle with//Создаёт копию с изменнёнными свойствами
            {
               Title = updated.Title,
               Body = updated.Body
            };
            await repository.UpdateArticleAsync(article);
            return NoContent();
        }
        [HttpDelete("{id}")]
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
