using System;
using System.Collections.Generic;

#nullable disable

namespace CapaDatos.Models
{
    public partial class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Excerpt { get; set; }
        public DateTime PublishDate { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
