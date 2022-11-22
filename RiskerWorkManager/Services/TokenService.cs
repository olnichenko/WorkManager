using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RiskerWorkManager.ConfigurationSettings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkManagerDal.Models;

namespace RiskerWorkManager.Services
{
    public class TokenService
    {
        private readonly JWTTokenSettings _tokenSettings;
        public TokenService(JWTTokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email) }),
                Expires = DateTime.UtcNow.AddHours(_tokenSettings.ValidTokenHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _tokenSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetEmailFromToken(string token)
        {
            if (token == null)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.SecretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _tokenSettings.Issuer,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userEmail = jwtToken.Claims.First(x => x.Type == "email").Value;

                return userEmail;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var userEmail = GetEmailFromToken(token);
                return !string.IsNullOrEmpty(userEmail);
            }
            catch
            {
                return false;
            }
        }
    }
}
