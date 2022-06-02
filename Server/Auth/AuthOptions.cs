using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "Modsen"; 
        public const string AUDIENCE = "https://localhost:5001"; 
        const string KEY = "secretKeyForModsenApi";   
        public const int LIFETIME = 30; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
