using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Entities;
using YesterdayTimesApi.Entities.JWTtoken;
using YesterdayTimesApi.Pagination;

namespace YesterdayTimesApi.Data
{
    public interface IRepository
    {
        //Article
        Task<Article> GetArticleAsync(Guid id);
        Task<IEnumerable<Article>> GetArticlesAsync(ArticleQueryParameters parameters);
        Task CreateArticleAsync(Article item);
        Task UpdateArticleAsync();
        Task DeleteArticleAsync(Guid id);

        //Category
        Task<Category> GetCategoryAsync(Guid id);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task CreateCategoryAsync(Category item);
        //Task UpdateCategoryAsync();
        Task DeleteCategoryAsync(Guid id);

        //Creator
        Task<Creator> GetCreatorAsync(Guid id);
        Task CreateCreatorAsync(Creator item);
        Task UpdateCreatorAsync();
        Task<bool> IsValidAdminAsync(UserMetaData user);
        Task<IEnumerable<Creator>> GetCreatorsAsync(CreatorQueryParameters parameters);
        Task DeleteCreatorAsync(Guid id);

        //User
        Task RegistrateUserAsync(User item);
        Task<UserRefreshTokens> AddUserRefreshTokens(UserRefreshTokens user);
        Task DeleteUserRefreshTokens(string username, string refreshToken);
        Task<UserRefreshTokens> GetSavedRefreshTokens(string username, string role, string refreshToken);
        int SaveCommit();
        Task<bool> IsValidUserAsync(UserMetaData user);
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync(UserQueryParameters parameters);
        Task<IEnumerable<User>> GetUsersByCategory(Guid id);
        Task UpdateUserAsync();
        Task DeleteUserAsync(Guid id);
    }
}
