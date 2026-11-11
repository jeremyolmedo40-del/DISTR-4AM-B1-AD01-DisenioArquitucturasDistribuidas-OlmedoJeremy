using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedido.API.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(150)]
        [Column("Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("Cedula")]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("Correo")]
        public string Correo { get; set; } = string.Empty;
    }

    [Table("PedidoM")]
    public class PedidoM
    {
        [Key]
        [Column("IdPedido")]
        public int IdPedido { get; set; }

        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Column("Fecha")]
        public DateTime Fecha { get; set; }

        [Column("Total", TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Estado")]
        public string Estado { get; set; } = string.Empty; 

        [ForeignKey("IdCliente")]
        public virtual Cliente? Cliente { get; set; }
    }

    [Table("DetallePedido")]
    public class DetallePedido
    {
        [Key]
        [Column("IdDetalle")]
        public int IdDetalle { get; set; }

        [Column("IdPedido")]
        public int IdPedido { get; set; }

        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Column("PrecioUnitario", TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("IdPedido")]
        public virtual PedidoM? PedidoM { get; set; }
    }
}