using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record ArticleDTO
    {
        public Guid Id { get; init; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTimeOffset createdDate { get; init; }
        public List<Creator> Creators { get; set; } = new();
        public Category Category { get; set; }
    }
}
