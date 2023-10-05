using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projeto2.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projeto2.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;    
        }

        private string TokenGenerator()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Key:JwtKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim("Store", "admin"),
                    new Claim(ClaimTypes.Role, "admin")
                }),
                Issuer = "AAA",
                Audience = "https://localhost:7078",
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpPost]
        [AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.Route("login")]
        public ActionResult PostLogin([FromBody] PostAuthLoginModel model)
        {
            if (!ModelState.IsValid) 
            {
               return BadRequest(ModelState);
            }
            else
            {
                if (model.login == "admin" &&  model.password == "admin") 
                {
                    string token = TokenGenerator();

                    return Ok(token);                
                }
                else
                {
                    return BadRequest("O login e/ou senha inválidos");
                }
            }      
        }
    }
}
