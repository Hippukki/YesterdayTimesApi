using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using YesterdayTimesApi.Data;
using YesterdayTimesApi.Entities;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using YesterdayTimesApi.Entities.JWTtoken;

namespace YesterdayTimesApi.Controllers
{
	[Authorize]
	[Route("Authentication")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IJWTManagerRepository jWTManager;
		private readonly IRepository repository;

		public AuthenticationController(IJWTManagerRepository jWTManager,  IRepository repository)
		{
			this.jWTManager = jWTManager;
			this.repository = repository;
		}

		[HttpGet("users")]
		public async Task<IEnumerable<UserDTO>> GetAsync()
		{
			var users = (await repository.GetUsersAsync()).Select(user => user.UserAsDTO());
			return users;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public async Task<IActionResult> AuthenticateAsync(UserDTO usersdata)
		{
			var validUser = await repository.IsValidUserAsync(usersdata);

			if (!validUser)
			{
				return Unauthorized("Incorrect username or password!");
			}

			var token = jWTManager.GenerateToken(usersdata.Email);

			if (token == null)
			{
				return Unauthorized("Invalid Attempt!");
			}

			// saving refresh token to the db
			UserRefreshTokens obj = new UserRefreshTokens
			{
				RefreshToken = token.Refresh_Token,
				UserName = usersdata.Email
			};

			await repository.AddUserRefreshTokens(obj);
			repository.SaveCommit();
			return Ok(token);
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("refresh")]
		public async Task<IActionResult> Refresh(Tokens token)
		{
			var principal = jWTManager.GetPrincipalFromExpiredToken(token.Access_Token);
			var username = principal.Identity?.Name;

			//retrieve the saved refresh token from database
			var savedRefreshToken = await repository.GetSavedRefreshTokens(username, token.Refresh_Token);

			if (savedRefreshToken.RefreshToken != token.Refresh_Token)
			{
				return Unauthorized("Invalid attempt!");
			}

			var newJwtToken = jWTManager.GenerateRefreshToken(username);

			if (newJwtToken == null)
			{
				return Unauthorized("Invalid attempt!");
			}

			// saving refresh token to the db
			UserRefreshTokens obj = new UserRefreshTokens
			{
				RefreshToken = newJwtToken.Refresh_Token,
				UserName = username
			};

			await repository.DeleteUserRefreshTokens(username, token.Refresh_Token);
			await repository.AddUserRefreshTokens(obj);
			repository.SaveCommit();

			return Ok(newJwtToken);
		}
	}
}
