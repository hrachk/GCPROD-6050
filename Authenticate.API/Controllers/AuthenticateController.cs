using Authenticate.API.Models;
using AuthTutorial.Auth.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
 

namespace Authenticate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _authOptions;
        public AuthenticateController(IOptions<AuthOptions> authOptions )
        {
            _authOptions = authOptions;
        }

        // In Memory Repository , // Virtual Database
        private List<Account> Accounts => new List<Account>
       {
           new Account(){
               id = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"), 
               Email ="User@mail.com" , 
               Password = "user",  //TODO: Cript
               Roles = new Role[]{  Role.User} },

           new Account(){ 
               id = Guid.Parse("7b0a1ec1-80f5-46b5-a108-fb938d3e26c0"), 
               Email ="User1@mail.com" , 
               Password = "user1",   //TODO: Cript
               Roles = new Role[]{  Role.User}  },

           new Account(){
               id = Guid.Parse(new Guid().ToString()),// 
               Email ="User2@mail.com" , 
               Password = "user2",  //TODO: Cript
               Roles = new Role[]{  Role.Admin} }
       };

        [Route("login")] // path to this method
        [HttpPost]
        public IActionResult Login([FromBody] Login request) //
        {
            var user = AuthenticateUser(request.Email, request.Password);
            if (user!=null)
            {
                // generate JWT token
                var token = GenerateJWT(user);
                return Ok(new 
                {
                    access_token = token 
                });
            }
            return Unauthorized();  
        } 

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        private string GenerateJWT(Account user)
        {
            var authParams = _authOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.id.ToString())
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }
            var token = new JwtSecurityToken(
                authParams.Issuer, 
                authParams.Audience, 
                claims, 
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
