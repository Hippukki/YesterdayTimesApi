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
        Task<IEnumerable<Article>> GetArticlesAsync();
        Task CreateArticleAsync(Article item);
        Task UpdateArticleAsync();
        Task DeleteArticleAsync(Guid id);

        //Category
        Task<Category> GetCategoryAsync(Guid id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task CreateCategoryAsync(Category item);
        Task UpdateCategoryAsync();
        Task DeleteCategoryAsync(Guid id);

        //Creator
        Task<Creator> GetCreatorAsync(Guid id);
        Task CreateCreatorAsync(Creator item);
        Task UpdateCreatorAsync();
        Task<IEnumerable<Creator>> GetCreatorsAsync();
    }
}
