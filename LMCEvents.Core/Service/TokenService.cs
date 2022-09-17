using LMCEvents.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMCEvents.Core.Service
{
    public class TokenService : ITokenService
    {
        private const string SECRET_KEY = "secretKey";
        private const string ISSUER = "Issuer";
        private const string AUDIENCE = "Audience";


        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateTokenEvents(string name, string permission)
        {
            var Key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>(SECRET_KEY));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetValue<string>(ISSUER),
                Audience = _configuration.GetValue<string>(AUDIENCE),
                Expires = DateTime.UtcNow.AddHours(2),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, permission)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
