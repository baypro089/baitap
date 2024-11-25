using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<BookDTO> Books { get; }

        DbSet<Genre> Genres { get; }

        DbSet<Catalogue> Catalogues { get; }

        DbSet<BookCatalogueDTO> BookCatalogues { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
