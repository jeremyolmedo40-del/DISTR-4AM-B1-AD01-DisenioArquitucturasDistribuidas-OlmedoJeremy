using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Producto
{
    [Key]
    [Column("IdProducto")]
    public int IdProducto { get; set; }
    [Required]
    [StringLength(100)]
    [Column("NombreProducto")]
    public string NombreProducto { get; set; } = string.Empty;
    [Column("Precio")]
    public decimal Precio { get; set; }
}

namespace Inventario.Api.Models
{
    [Table("Bodega")]
    public class Bodega
    {
        [Key]
        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NombreBodega")]
        public string NombreBodega { get; set; } = string.Empty;
    }

    [Table("Existencias")]
    public class Existencia
    {
        [Key]
        [Column("IdExistencia")]
        public int IdExistencia { get; set; }

        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [ForeignKey("IdBodega")]
        public virtual Bodega? Bodega { get; set; }
    }

    [Table("MovimientosInv")]
    public class MovimientoInv
    {
        [Key]
        [Column("IdMovimientoInv")]
        public int IdMovimientoInv { get; set; }

        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(50)]
        [Column("TipoMovimiento")]
        public string TipoMovimiento { get; set; } = string.Empty;

        [ForeignKey("IdBodega")]
        public virtual Bodega? Bodega { get; set; }
    }

    [Table("ReservaStock")]
    public class ReservaStock
    {
        [Key]
        [Column("IdReservaStock")]
        public int IdReservaStock { get; set; }

        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Column("CantidadReservada")]
        public int CantidadReservada { get; set; }

        [ForeignKey("IdBodega")]
        public virtual Bodega? Bodega { get; set; }
    }
}