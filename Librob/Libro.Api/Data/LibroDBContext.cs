using Libro.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Libro.Api.Data
{
    public class LibroDBContext : DbContext
    {
        public LibroDBContext(DbContextOptions<LibroDBContext> options) : base(options)
        {
        }

        public DbSet<Libros> Libros { get; set; }
    }
}