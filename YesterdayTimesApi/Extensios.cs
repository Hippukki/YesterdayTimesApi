using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Entities;

namespace YesterdayTimesApi
{
    public static class Extensios
    {
        public static ArticleDTO ArticleAsDTO(this Article entuty)
        {
            return new ArticleDTO
            {
                Id = entuty.Id,
                Title = entuty.Title,
                Body = entuty.Body,
                createdDate = entuty.createdDate,
                Creators = entuty.Creators,
                Categories = entuty.Categories
            };
        } 
    }
}
