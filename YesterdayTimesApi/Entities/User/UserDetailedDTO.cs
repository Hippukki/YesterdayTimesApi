using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record UserDetailedDTO
    {
        public Guid Id { get; init; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; init; }
        public List<CategoryDTO> Categories { get; set; } = new();
    }
}
