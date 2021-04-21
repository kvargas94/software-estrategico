using System;
using System.Collections.Generic;

#nullable disable

namespace CapaDatos.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Book IdBookNavigation { get; set; }
    }
}
