using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWalletV2.Api.Helpers
{
    public class GetToken
    {
        private readonly IConfiguration _configuration;
        public GetToken(IConfiguration configuration)
        {
            _configuration = configuration;
            Token = GetResultToken();
        }
        public string Token { get; set; }
        public string GetResultToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokendata = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"], null,
              DateTime.Now,
              expires: DateTime.Now.AddMinutes(5),
              signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokendata);
            return token;
        }
    }
}
