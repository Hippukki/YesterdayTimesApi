using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record CreatorDetailedDTO
    {
        public Guid Id { get; init; }
        public string fullName { get; set; }
        public List<ArticleDTO> Articles { get; set; }
    }
}
