using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Entities;

namespace YesterdayTimesApi
{
    public static class Extensios
    {
        public static ArticleDTO ArticleAsDTO(this Article entity)
        {
            return new ArticleDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Body = entity.Body,
                createdDate = entity.createdDate,
                Creators = entity.Creators,
                Categories = entity.Categories
            };
        }
        public static CategoryDTO CategoryAsDTO(this Category entity)
        {
            return new CategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Articles = entity.Articles,
                Users = entity.Users
            };
        }
    }
}
