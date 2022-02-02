using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public class UpdatedUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Guid idCategory { get; set; }
    }
}
