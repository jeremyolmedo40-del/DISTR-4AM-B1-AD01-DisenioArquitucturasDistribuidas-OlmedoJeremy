using Microsoft.EntityFrameworkCore;
using Inventario.Models;

namespace Inventario.Data
{
    public class InventarioDBContext : DbContext
    {
        public InventarioDBContext(DbContextOptions<InventarioDBContext> options) : base(options)
        {
        }

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Existencia> Existencias { get; set; }
        public DbSet<MovimientoInv> MovimientosInv { get; set; }
        public DbSet<ReservaStock> ReservaStock { get; set; }
    }
}