using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace YesterdayTimesApi
{
    public class AuthOptions
    {
        public const string ISSUER = "YTAuthServer";
        public const string AUDIENCE = "YesterdayTimesApi";
        const string KEY = "W5rq#8cgnqc7uG#oh3mDG{L6I{";
        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
