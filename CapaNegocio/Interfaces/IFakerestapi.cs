using CapaDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Interfaces
{
    public interface IFakerestapi
    {
        bool Login(string userName, string password);
        bool Sincronizar();
        List<Author> ListaAutores();
        List<Book> ListaBooks(int book_id, DateTime fecha_inicio, DateTime fecha_fin);
    }
}
