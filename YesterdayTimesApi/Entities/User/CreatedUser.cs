using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record CreatedUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        public Guid SelctedCategoryId1 { get; set; }
        public Guid SelctedCategoryId2 { get; set; }
        public Guid SelctedCategoryId3 { get; set; }
        public Guid SelctedCategoryId4 { get; set; }
        public Guid SelctedCategoryId5 { get; set; }
    }
}
