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
            var article = await Articles.FindAsync(id);
            foreach( var c in article.Creators)
            {
                article.Creators.Add(c);
            }
            return article;


            //var courses = db.Courses.Include(c => c.Students).ToList();
            //// выводим все курсы
            //foreach (var c in courses)
            //{
            //    Console.WriteLine($"Course: {c.Name}");
            //    // выводим всех студентов для данного кура
            //    foreach (Student s in c.Students)
            //        Console.WriteLine($"Name: {s.Name}");
            //    Console.WriteLine("-------------------");
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await Articles.ToListAsync();
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
