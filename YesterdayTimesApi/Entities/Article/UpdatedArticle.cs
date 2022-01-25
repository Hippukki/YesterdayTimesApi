using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record UpdatedArticle
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
