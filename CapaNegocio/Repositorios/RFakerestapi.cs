using AutoMapper;
using CapaDatos.Models;
using CapaNegocio.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Repositorios
{
    public class RFakerestapi :IFakerestapi
    {
        private readonly db_seContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public RFakerestapi(db_seContext context, IMapper mapper , IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public class mUsers
        {
            public int id { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
        }
        public bool Login(string userName , string password)
        {
            //string respuesta = "";
            string api_url = _configuration["Api:Url"];
            List<mUsers> listaUsuarios = new List<mUsers>();
            var url = api_url + $"/api/v1/Users";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {                    
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        listaUsuarios = JsonConvert.DeserializeObject<List<mUsers>>(responseBody);
                        var usuario = listaUsuarios.FirstOrDefault(x => x.userName == userName && x.password == password);
                        if(usuario != null)
                        {
                            return true;
                        }
                    }                    
                }
            }

            return false;
        }
        public bool Sincronizar()
        {
            string api_url = _configuration["Api:Url"];
            _context.Authors.RemoveRange(_context.Authors);
            _context.SaveChanges();
            _context.Books.RemoveRange(_context.Books);
            _context.SaveChanges();

            var url = api_url + $"/api/v1/Books";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        List<Book> libros = new List<Book>(JsonConvert.DeserializeObject<List<Book>>(responseBody));

                        _context.Books.AddRange(libros);
                        _context.SaveChanges();
                    }
                }
            }

            url = api_url + $"/api/v1/Authors";
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        List<Author> autores = new List<Author>(JsonConvert.DeserializeObject<List<Author>>(responseBody));
                      
                        _context.Authors.AddRange(autores);
                        _context.SaveChanges();
                    }
                }
            }

            return false;
        }

        public List<Author> ListaAutores()
        {
            List<Author> lista = new List<Author>();
            lista = _context.Authors.ToList();
            return lista;
        }

        public List<Book> ListaBooks(int book_id, DateTime fecha_inicio, DateTime fecha_fin)
        {
            List<Book> lista = new List<Book>();
            lista = _context.Books.Where(x => x.Id == book_id && x.PublishDate > fecha_inicio && x.PublishDate < fecha_fin).ToList();
            return lista;
        }
    }
}
