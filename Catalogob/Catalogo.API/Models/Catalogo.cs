using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo.Api.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [Column("IdCategoria")]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NombreCategoria")]
        public string NombreCategoria { get; set; } = string.Empty;

        [StringLength(255)]
        [Column("Descripcion")]
        public string? Descripcion { get; set; }
    }

    [Table("Marca")]
    public class Marca
    {
        [Key]
        [Column("IdMarca")]
        public int IdMarca { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NombreMarca")]
        public string NombreMarca { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("PaisOrigen")]
        public string? PaisOrigen { get; set; }
    }

    [Table("Producto")]
    public class Producto
    {
        [Key]
        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Column("IdMarca")]
        public int IdMarca { get; set; }

        [Column("IdCategoria")]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NombreProducto")]
        public string NombreProducto { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("SKU")]
        public string? SKU { get; set; }

        [Column("Precio", TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Estado")]
        public string Estado { get; set; } = "Activo";

        // relacion con Marca y Categoria
        [ForeignKey("IdMarca")]
        public virtual Marca? Marca { get; set; }

        [ForeignKey("IdCategoria")]
        public virtual Categoria? Categoria { get; set; }
    }
}