using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YesterdayTimesApi.Entities.JWTtoken;

namespace YesterdayTimesApi.Data
{
	public interface IJWTManagerRepository
	{
		Tokens GenerateToken(string userName, string role);
		Tokens GenerateRefreshToken(string userName, string role);
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}
