using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BookCatalogue
    {
        public int Id { get; set; } 
        public DateTime CreatedOn { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CatalogueId { get; set; }
        public Catalogue Catalogue { get; set; }

        public int IsActive { get; set; }

       
    }
}