using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record User
    {
        public Guid Id { get; init; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; init; }
        public List<Category> Categories { get; set; } = new();
    }
}
