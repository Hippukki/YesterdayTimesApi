using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record CategoryDetailedDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<ArticleDTO> Articles { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
