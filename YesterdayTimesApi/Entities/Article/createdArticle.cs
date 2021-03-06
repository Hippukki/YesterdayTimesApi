using System;
using System.ComponentModel.DataAnnotations;

namespace YesterdayTimesApi.Entities
{
    public record CreatedArticle
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public Guid idCategory { get; set; }
    }
}
