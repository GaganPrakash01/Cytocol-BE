using Cytocol.Domain.DTO;
using Cytocol.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Cytocol.Shared.Authentication
{
    public class JWTAuthenticator : ITokenService
    {
        private readonly string _issuer;
        private readonly string _secret;

        public JWTAuthenticator()
        {
            _issuer = ConfigurationManager.AppSettings["issuer"];
            _secret = ConfigurationManager.AppSettings["secret"];
        }

        public string GenerateToken(int expiryInMinutes, params TokenClaims[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claimList = new List<Claim>();
            claims.ToList().ForEach(c =>
            {
                claimList.Add(new Claim(ClaimTypes.Email, c.Email));
                claimList.Add(new Claim(ClaimTypes.Role, c.Roles));
                claimList.Add(new Claim("userId", c.UserId.ToString()));
            });

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _issuer,
                IssuedAt = DateTime.UtcNow,
                Subject = new ClaimsIdentity(claimList.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expiryInMinutes)),
                SigningCredentials = credential
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            //Define Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //Get the key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            //Specify the token validation conditions
            var validationConditions = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidIssuer = _issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateAudience = false
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationConditions, out _);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
