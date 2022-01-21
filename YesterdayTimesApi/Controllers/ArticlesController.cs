using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<ActionResult<Article>> CreateArticleAsync(createdArticle created)
        {
            Article article = new()
            {
                Id = Guid.NewGuid(),
                Title = created.Title,
                Body = created.Body,
                createdDate = DateTimeOffset.UtcNow
            };
            await repository.CreateArticleAsync(article);
            return CreatedAtAction(nameof(GetArticleAsync), new { Id = article.Id }, article);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticleAsync(Guid id)
        {
            var article = await repository.GetArticleAsync(id);
            if (article is null)
            {
                return NotFound();
            }
            return article;
        }
    }
}
