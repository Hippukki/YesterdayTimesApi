using Microsoft.EntityFrameworkCore;
using YesterdayTimesApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Data
{
    public class NewsPortalContext : DbContext, IRepository
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<User> Users { get; set; }
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
            return await Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await Categories.ToListAsync();
        }

        public async Task CreateCategoryAsync(Category item)
        {
            await Categories.AddAsync(item);
            SaveChanges();
        }

        public async Task UpdateCategoryAsync()
        {
            await SaveChangesAsync();
        }

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
            return await Creators.FindAsync(id);
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
            return await Creators.ToListAsync();
        }
        #endregion
    }
}
