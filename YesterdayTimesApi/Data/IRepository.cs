using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Entities;

namespace YesterdayTimesApi.Data
{
    public interface IRepository
    {
        //Article
        Task<Article> GetArticleAsync(Guid id);
        Task<IEnumerable<Article>> GetArticleAsync();
        Task CreateArticleAsync(Article item);
        Task UpdateArticleAsync(Article item);
        Task DeleteArticleAsync(Guid id);

    }
}
