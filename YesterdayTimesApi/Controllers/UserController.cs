using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;

namespace YesterdayTimesApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository repository;

        public UserController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost("create")]
        public async Task<ActionResult> RegisterUserAsync(CreatedUser created)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Email = created.Email,
                Password = created.Password
            };
            await repository.RegistrateUserAsync(user);
            return NoContent();
        }
    }
}
