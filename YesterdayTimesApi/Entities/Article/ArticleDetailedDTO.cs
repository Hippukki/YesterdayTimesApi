using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record ArticleDetailedDTO
    {
        public Guid Id { get; init; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTimeOffset createdDate { get; init; }
        public List<CreatorDTO> Creators { get; set; } = new();
        public CategoryDTO Category { get; set; }
    }
}
