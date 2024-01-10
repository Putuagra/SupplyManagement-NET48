using Microsoft.IdentityModel.Tokens;
using SupplyManagement_NET48.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Configuration;

namespace SupplyManagement_NET48.Utilities.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        public TokenHandler()
        {
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var keyBytes = Encoding.UTF8.GetBytes(WebConfigurationManager.AppSettings["JWTService:Key"]);
            var signingKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: WebConfigurationManager.AppSettings["JWTService:Issuer"],
                audience: WebConfigurationManager.AppSettings["JWTService:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}