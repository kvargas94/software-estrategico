using CapaNegocio.Interfaces;
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
        public class mUsers
        {
            public int id { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
        }
        public bool Login(string userName , string password)
        {
            //string respuesta = "";
            List<mUsers> listaUsuarios = new List<mUsers>();
            var url = $"https://fakerestapi.azurewebsites.net/api/v1/Users";
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
        public bool Sincronizar(string userName, string password)
        {
            //string respuesta = "";
            List<mUsers> listaUsuarios = new List<mUsers>();
            var url = $"https://fakerestapi.azurewebsites.net/api/v1/Users";
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
                        if (usuario != null)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
