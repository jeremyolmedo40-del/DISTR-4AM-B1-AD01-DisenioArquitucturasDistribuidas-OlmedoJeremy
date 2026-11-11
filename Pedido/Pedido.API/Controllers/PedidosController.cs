using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedido.API.Data;
using Pedido.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pedido.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosDBContext _context;

        public PedidosController(PedidosDBContext context)
        {
            _context = context;
        }

        // ==========================================
        // --- CLIENTES ---
        // ==========================================

        [Tags("Clientes")]
        [HttpGet("clientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes() => await _context.Clientes.AsNoTracking().ToListAsync();

        [Tags("Clientes")]
        [HttpGet("clientes/{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            return cliente == null ? NotFound() : cliente;
        }

        [Tags("Clientes")]
        [HttpPost("clientes")]
        public async Task<ActionResult<Cliente>> CrearCliente(Cliente cliente)
        {
            cliente.IdCliente = 0;
            _context.Clientes.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }

        [Tags("Clientes")]
        [HttpPut("clientes/{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente) return BadRequest();
            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Tags("Clientes")]
        [HttpDelete("clientes/{id}")]
        public async Task<IActionResult> BorrarCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            _context.Clientes.Remove(cliente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return NoContent();
        }


        // ==========================================
        // --- PEDIDOS ---
        // ==========================================

        [Tags("Pedidos")]
        [HttpGet("pedidos")]
        public async Task<ActionResult<IEnumerable<PedidoM>>> GetPedidos() => await _context.Pedidos.Include(p => p.Cliente).AsNoTracking().ToListAsync();

        [Tags("Pedidos")]
        [HttpGet("pedidos/{id}")]
        public async Task<ActionResult<PedidoM>> GetPedidoById(int id)
        {
            var pedido = await _context.Pedidos.Include(p => p.Cliente).FirstOrDefaultAsync(p => p.IdPedido == id);
            return pedido == null ? NotFound() : pedido;
        }

        [Tags("Pedidos")]
        [HttpPost("pedidos")]
        public async Task<ActionResult<PedidoM>> CrearPedido(PedidoM pedido)
        {
            pedido.IdPedido = 0;
            pedido.Cliente = null;

            _context.Pedidos.Add(pedido);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.IdPedido }, pedido);
        }

        [Tags("Pedidos")]
        [HttpPut("pedidos/{id}")]
        public async Task<IActionResult> ActualizarPedido(int id, PedidoM pedido)
        {
            if (id != pedido.IdPedido) return BadRequest();
            pedido.Cliente = null;
            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Tags("Pedidos")]
        [HttpDelete("pedidos/{id}")]
        public async Task<IActionResult> BorrarPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            _context.Pedidos.Remove(pedido);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return NoContent();
        }


        // ==========================================
        // --- DETALLE PEDIDOS ---
        // ==========================================

        [Tags("Detalles")]
        [HttpGet("detalles")]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetalles() => await _context.DetallePedidos.Include(d => d.PedidoM).AsNoTracking().ToListAsync();

        [Tags("Detalles")]
        [HttpGet("detalles/{id}")]
        public async Task<ActionResult<DetallePedido>> GetDetalle(int id)
        {
            var detalle = await _context.DetallePedidos.Include(d => d.PedidoM).FirstOrDefaultAsync(d => d.IdDetalle == id);
            return detalle == null ? NotFound() : detalle;
        }

        [Tags("Detalles")]
        [HttpPost("detalles")]
        public async Task<ActionResult<DetallePedido>> CrearDetalle(DetallePedido detalle)
        {
            detalle.IdDetalle = 0;
            detalle.PedidoM = null;

            _context.DetallePedidos.Add(detalle);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetDetalle), new { id = detalle.IdDetalle }, detalle);
        }

        [Tags("Detalles")]
        [HttpPut("detalles/{id}")]
        public async Task<IActionResult> ActualizarDetalle(int id, DetallePedido detalle)
        {
            if (id != detalle.IdDetalle) return BadRequest();
            detalle.PedidoM = null;
            _context.Entry(detalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Tags("Detalles")]
        [HttpDelete("detalles/{id}")]
        public async Task<IActionResult> BorrarDetalle(int id)
        {
            var detalle = await _context.DetallePedidos.FindAsync(id);
            if (detalle == null) return NotFound();

            _context.DetallePedidos.Remove(detalle);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}