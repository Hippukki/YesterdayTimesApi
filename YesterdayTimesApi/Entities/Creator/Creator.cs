using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record Creator
    {
        public Guid Id { get; init; }
        public string fullName { get; set; }
        public List<Article> Articles { get; set; } = new();
    }
}
