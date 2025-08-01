using Baykasoglu.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Baykasoglu.API.Repositories.Implementation
{
    public class tokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<tokenRepository> logger;

        public tokenRepository(IConfiguration configuration, ILogger<tokenRepository> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            /*
            //Create Claims
            var claims = new List<Claim>
           {
           new Claim(ClaimTypes.Email, user.Email),
           };

            claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role, role)));
            //JWT Security token paramaters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );


            //return token 
            return new JwtSecurityTokenHandler().WriteToken(token);
        */

            Console.WriteLine("🔐 TOKEN KEY (from tokenRepository):");
            Console.WriteLine(configuration["Jwt:Key"]);

            logger.LogInformation("🔐 TOKEN KEY (from tokenRepository): {Key}", configuration["Jwt:Key"]);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email)
    };

            // ASP.NET Identity için gerekli
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // JWT middleware için gerekli
            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var keyString = configuration["Jwt:Key"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            Console.WriteLine("🟩 Token wfwefwfKEY: " + keyString);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

    }
}
