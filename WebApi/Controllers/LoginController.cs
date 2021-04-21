
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFakerestapi _ifakerestapi;
        public LoginController(IConfiguration configuration,IFakerestapi fakerestapi)
        {
            _ifakerestapi = fakerestapi;
            _configuration = configuration;
        }
        public class mLogin
        {
            public string userName { get; set; }
            public string password { get; set; }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] mLogin usuario)
        {
            if(usuario.userName == null || usuario.password == null)
            {
                return BadRequest("Usuario o contraseña invalidos");
            }
            try
            {
                if(_ifakerestapi.Login(usuario.userName, usuario.password))
                {
                    string ValidIssuer = _configuration["ApiAuth:Issuer"];
                    string ValidAudience = _configuration["ApiAuth:Audience"];
                    SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"]));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: ValidIssuer,
                        audience: ValidAudience,
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signingCredentials
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return Unauthorized();
                }
               
            }
            catch(Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }
       
    }
}
