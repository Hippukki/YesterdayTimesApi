using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
