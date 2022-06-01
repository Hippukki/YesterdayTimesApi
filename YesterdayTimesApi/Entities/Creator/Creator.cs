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
        public string Password { get; set; }
        public string Role { get; init; }
        public List<Article> Articles { get; set; } = new();
    }
}
