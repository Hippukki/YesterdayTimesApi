using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Entities;
using YesterdayTimesApi.Entities.JWTtoken;

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
        //Task UpdateCategoryAsync();
        Task DeleteCategoryAsync(Guid id);

        //Creator
        Task<Creator> GetCreatorAsync(Guid id);
        Task CreateCreatorAsync(Creator item);
        Task UpdateCreatorAsync();
        Task<IEnumerable<Creator>> GetCreatorsAsync();

        //User
        Task RegistrateUserAsync(User item);
        Task<UserRefreshTokens> AddUserRefreshTokens(UserRefreshTokens user);
        Task DeleteUserRefreshTokens(string username, string refreshToken);
        Task<UserRefreshTokens> GetSavedRefreshTokens(string username, string role, string refreshToken);
        int SaveCommit();
        Task<bool> IsValidUserAsync(UserDTO user);
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task UpdateUserAsync();
        Task DeleteUserAsync(Guid id);
    }
}
