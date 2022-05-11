using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;

namespace YesterdayTimesApi
{
    public static class Extensios
    {
        #region [Article Extensios]
        public static ArticleDTO ArticleAsDTO(this Article entity)
        {

            return new ArticleDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Body = entity.Body,
                createdDate = entity.createdDate
            };
        }
        public static ArticleDetailedDTO ArticleAsDetailedDTO(this Article entity)
        {

            return new ArticleDetailedDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Body = entity.Body,
                createdDate = entity.createdDate,
                Creators = entity.Creators.Select(e => e.CreatorAsDTO()).ToList(),
                Category = entity.Category.CategoryAsDTO()
            };
        }
        #endregion

        #region [Category Extensios]
        public static CategoryDTO CategoryAsDTO(this Category entity)
        {
            return new CategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
        public static CategoryDetailedDTO CategoryAsDetailedDTO(this Category entity)
        {
            return new CategoryDetailedDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Articles = entity.Articles.Select(e => e.ArticleAsDTO()).ToList(),
                Users = entity.Users.Select(e => e.UserAsDTO()).ToList()
            };
        }
        #endregion

        #region [Creator Extensios]
        public static CreatorDTO CreatorAsDTO(this Creator entity)
        {
            return new CreatorDTO
            {
                Id = entity.Id,
                fullName = entity.fullName
            };
        }
        public static CreatorDetailedDTO CreatorAsDetailedDTO(this Creator entity)
        {
            return new CreatorDetailedDTO
            {
                Id = entity.Id,
                fullName = entity.fullName,
                Articles = entity.Articles.Select(e => e.ArticleAsDTO()).ToList()
            };
        }
        #endregion

        #region [User Extensios]
        public static UserDTO UserAsDTO(this User entity)
        {
            return new UserDTO
            {
                Id = entity.Id,
                Email = entity.Email,
                Password = entity.Password,
                Role = entity.Role
            };
        }
        public static UserDetailedDTO UserAsDetailedDTO(this User entity)
        {
            return new UserDetailedDTO
            {
                Id = entity.Id,
                Email = entity.Email,
                Password = entity.Password,
                Role = entity.Role,
                Categories = entity.Categories.Select(e => e.CategoryAsDTO()).ToList()
            };
        }
        #endregion
    }
}
