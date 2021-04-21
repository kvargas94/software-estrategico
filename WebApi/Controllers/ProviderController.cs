using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IFakerestapi _ifakerestapi;
        public ProviderController(IFakerestapi fakerestapi)
        {
            _ifakerestapi = fakerestapi;
        }
        [HttpGet]
        [Route("GetAutores")]
        public IActionResult GetAutores()
        {
            try
            {
                var response = _ifakerestapi.ListaAutores();
                if(response != null)
                {
                    return Ok( new { Content = response});
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBooks(int book_id, string fecha_inicio , string fecha_fin)
        {
            try
            {
                
                DateTime oFecha_inicio = DateTime.ParseExact(fecha_inicio,"yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                DateTime oFecha_fin = DateTime.ParseExact(fecha_fin, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                var response = _ifakerestapi.ListaBooks(book_id, oFecha_inicio , oFecha_fin);
                if(response != null)
                {
                    return Ok(new { Content = response });
                }
                else
                {
                    return NoContent();
                }
               
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPost]
        [Route("PostSync")]
        public IActionResult Post()
        {
            try
            {   if(_ifakerestapi.Sincronizar())
                {
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
              
               
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

     
    }
}
