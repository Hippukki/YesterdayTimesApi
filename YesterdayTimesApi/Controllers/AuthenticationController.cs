
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
	[Route("authentication")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IJWTManagerRepository jWTManager;
		private readonly IRepository repository;

		public AuthenticationController(IJWTManagerRepository jWTManager, IRepository repository)
		{
			this.jWTManager = jWTManager;
			this.repository = repository;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> AuthenticateAsync(UserMetaData usersdata)
		{
			var validUser = await repository.IsValidUserAsync(usersdata);
			var validAdmin = await repository.IsValidAdminAsync(usersdata);
			var role = "user";
			if (validUser == false)
			{
                if (validAdmin == false)
                {
					return Unauthorized("Incorrect username or password!");
				}
				role = "admin";
			}

			var token = jWTManager.GenerateToken(usersdata.Login, role);

			if (token == null)
			{
				return Unauthorized("Invalid Attempt!");
			}

			// saving refresh token to the db
			UserRefreshTokens obj = new()
            {
				RefreshToken = token.Refresh_Token,
				UserName = usersdata.Login,
				Role = role
			};

			await repository.AddUserRefreshTokens(obj);
			return Ok(token);
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("refresh")]
		public async Task<IActionResult> Refresh(Tokens token)
		{
			var principal = jWTManager.GetPrincipalFromExpiredToken(token.Access_Token);
			var username = principal.Identity?.Name;
			var role = "";
			foreach(Claim claim in principal.Claims)
            {
				if (claim.Type == ClaimTypes.Role)
				{
					role = claim.Value;
				}
			}

			//retrieve the saved refresh token from database
			var savedRefreshToken = await repository.GetSavedRefreshTokens(username, role, token.Refresh_Token);

			if (savedRefreshToken.RefreshToken == null || savedRefreshToken.RefreshToken != token.Refresh_Token)
			{
				return Unauthorized("Invalid attempt!");
			}

			var newJwtToken = jWTManager.GenerateRefreshToken(username, role);

			if (newJwtToken == null)
			{
				return Unauthorized("Invalid attempt!");
			}

			// saving refresh token to the db
			UserRefreshTokens obj = new()
            {
				RefreshToken = newJwtToken.Refresh_Token,
				UserName = username,
				Role = role
			};
			await repository.DeleteUserRefreshTokens(username, token.Refresh_Token);
			await repository.AddUserRefreshTokens(obj);
			repository.SaveCommit();
			return Ok(newJwtToken);
		}
	}
}
