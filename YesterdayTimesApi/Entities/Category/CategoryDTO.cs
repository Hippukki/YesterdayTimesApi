using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesterdayTimesApi.Entities
{
    public record CategoryDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
