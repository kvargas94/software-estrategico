using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IFakerestapi _ifakerestapi;
        public LoginController(IFakerestapi fakerestapi)
        {
            _ifakerestapi = fakerestapi;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string userName, string password)
        {
            try
            {
                var respuesta = _ifakerestapi.Login(userName, password);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
            
            
          
        }
    }
}
