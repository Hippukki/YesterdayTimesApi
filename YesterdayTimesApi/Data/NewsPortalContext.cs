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

        public async Task<Article> GetArticleAsync(Guid id)
        {
            return await Articles.Where(p => p.Id == id).SingleOrDefaultAsync();            
        }

        public async Task<IEnumerable<Article>> GetArticleAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreateArticleAsync(Article item)
        {
            await Articles.AddAsync(item);
            SaveChanges();
        }

        public async Task UpdateArticleAsync(Article item)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteArticleAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
