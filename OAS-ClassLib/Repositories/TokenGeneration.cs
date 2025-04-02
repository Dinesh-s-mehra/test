﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OAS_ClassLib;
using OAS_ClassLib.Models;

namespace JWT.Logic
{
    public class TokenGeneration
    {
        private readonly IConfiguration _configuration;
        public TokenGeneration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJWT(User user)
        {
            //Form Security Key and Credential
            var key = _configuration["ApiSettings:Secret"];
            var securedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var securityCredentials = new SigningCredentials(securedKey, SecurityAlgorithms.HmacSha256);

            //Define Claims with a List of Claims 
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //Unique Id
                new Claim("ContactNumber", user.ContactNumber)
            };

            //Define the Token Object
            var token = new JwtSecurityToken(
                issuer: "souravmaitra.com",
                audience: "Interns",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: securityCredentials
            );

            var tokenS = new JwtSecurityTokenHandler();
            return tokenS.WriteToken(token);
        }
    }
}