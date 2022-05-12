using Microsoft.EntityFrameworkCore;
using YesterdayTimesApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using YesterdayTimesApi.Entities.JWTtoken;

namespace YesterdayTimesApi.Data
{
    public class NewsPortalContext : DbContext, IRepository
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshTokens> UserRefreshToken { get; set; }
        public NewsPortalContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        #region [Article implementation]
        public async Task<Article> GetArticleAsync(Guid id)
        {
            var articles = await Articles.Include(a => a.Creators)
                .Include(a => a.Category)
                .ToListAsync();
            var article = (from a in articles where a.Id == id select a)
                .SingleOrDefault();
            return article;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            var articles = await Articles.Include(a => a.Creators)
                .Include(a => a.Category)
                .ToListAsync();
            return articles;
        }

        public async Task CreateArticleAsync(Article item)
        {
            await Articles.AddAsync(item);
            SaveChanges();
        }

        public async Task UpdateArticleAsync()
        {
            await SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(Guid id)
        {
            var existingItem = await Articles.FindAsync(id);
            Articles.Remove(existingItem);
            SaveChanges();
        }
        #endregion

        #region [Category implementation]
        public async Task<Category> GetCategoryAsync(Guid id)
        {
            var categories = await Categories.Include(c => c.Articles)
               .Include(c => c.Users)
               .ToListAsync();
            var category = (from c in categories where c.Id == id select c)
                .SingleOrDefault();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await Categories.Include(c => c.Articles)
               .Include(c => c.Users)
               .ToListAsync();
            return categories;
        }

        public async Task CreateCategoryAsync(Category item)
        {
            await Categories.AddAsync(item);
            SaveChanges();
        }

        //public async Task UpdateCategoryAsync()
        //{
        //    await SaveChangesAsync();
        //}

        public async Task DeleteCategoryAsync(Guid id)
        {
            var existingItem = await Categories.FindAsync(id);
            Categories.Remove(existingItem);
            SaveChanges();
        }
        #endregion

        #region Creator implementation
        public async Task<Creator> GetCreatorAsync(Guid id)
        {
            var creators = await Creators.Include(c => c.Articles)
                .ToListAsync();
            var creator = (from c in creators where c.Id == id select c)
                .SingleOrDefault();
            return creator;
        }
        public async Task CreateCreatorAsync(Creator item)
        {
            await Creators.AddAsync(item);
            SaveChanges();
        }
        public async Task UpdateCreatorAsync()
        {
            await SaveChangesAsync();
        }
        public async Task<IEnumerable<Creator>> GetCreatorsAsync()
        {
            var creators = await Creators.Include(c => c.Articles)
                .ToListAsync();
            return creators;
        }
        #endregion

        #region User implemention
        public async Task RegistrateUserAsync(User item)
        {
            var HashedPassword = item.HashUserPassword(item.Password);
            item.Password = HashedPassword;
            await Users.AddAsync(item);
            SaveChanges();
        }

        public async Task<UserRefreshTokens> AddUserRefreshTokens(UserRefreshTokens user)
        {
            await UserRefreshToken.AddAsync(user);
            return user;
        }

        public async Task DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = await UserRefreshToken.FirstOrDefaultAsync(x => x.UserName == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                UserRefreshToken.Remove(item);
            }
        }

        public async Task<UserRefreshTokens> GetSavedRefreshTokens(string username, string role, string refreshToken)
        {
            return await UserRefreshToken.FirstOrDefaultAsync(x => x.UserName == username && x.Role == role && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public int SaveCommit()
        {
            return SaveChanges();
        }

        public async Task<bool> IsValidUserAsync(UserDTO user)
        {
            var users = await GetUsersAsync();
            var currentUser = users.FirstOrDefault(u => u.Email.ToLower() ==
                user.Email.ToLower());
            if (currentUser is not null)
            {
                return currentUser.UserPasswordVerification(currentUser.Password, user.Password);
                
            }
            return false;

        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var users = await Users.Include(u => u.Categories)
               .ToListAsync();
            var user = (from u in users where u.Id == id select u)
                .SingleOrDefault();
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await Users.Include(u => u.Categories)
                .ToListAsync();
            return users;
        }

        public async Task UpdateUserAsync()
        {
            await SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var existingItem = await Users.FindAsync(id);
            Users.Remove(existingItem);
            SaveChanges();
        }
        #endregion
    }
}
