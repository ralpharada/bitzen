using Bitzen.Application.Contracts;
using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bitzen.Application.Services
{
    public class JwtService : IJwtService
    {
    
        public IJsonWebToken GenerateUsuarioToken(IUsuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = JwtSettings.AccessTokenExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return new JsonWebToken
            {
                AccessToken = accessToken,
                RefreshToken = CreateRefreshTokenUsuario(user.Id),
                ExpiresIn = (long)TimeSpan.FromMinutes(JwtSettings.ValidForMinutes).TotalSeconds
            };
        }

        private RefreshToken CreateRefreshTokenUsuario(int id)
        {
            var refreshToken = new RefreshToken
            {
                UsuarioId = id,
                DataExpiracao = JwtSettings.RefreshTokenExpiration,
            };

            string token;
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            refreshToken.Token = token.Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);

            return refreshToken;
        }
    }
}
