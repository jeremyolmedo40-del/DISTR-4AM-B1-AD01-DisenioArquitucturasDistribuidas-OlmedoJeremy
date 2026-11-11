using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public InventarioController(InventarioDBContext context)
        {
            _context = context;
        }

       
        // --- BODEGAS ---
      
        [Tags("Bodegas")]
        [HttpGet("bodegas")]
        public async Task<ActionResult<IEnumerable<Bodega>>> GetBodegas() => await _context.Bodegas.AsNoTracking().ToListAsync();

        [Tags("Bodegas")]
        [HttpGet("bodegas/{id}")]
        public async Task<ActionResult<Bodega>> GetBodega(int id)
        {
            var bodega = await _context.Bodegas.FindAsync(id);
            return bodega == null ? NotFound() : bodega;
        }

        [Tags("Bodegas")]
        [HttpPost("bodegas")]
        public async Task<ActionResult<Bodega>> CrearBodega(Bodega bodega)
        {
            _context.Bodegas.Add(bodega);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetBodega), new { id = bodega.IdBodega }, bodega);
        }

        [Tags("Bodegas")]
        [HttpPut("bodegas/{id}")]
        public async Task<IActionResult> ActualizarBodega(int id, Bodega bodega)
        {
            if (id != bodega.IdBodega) return BadRequest();
            _context.Entry(bodega).State = EntityState.Modified;

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

        [Tags("Bodegas")]
        [HttpDelete("bodegas/{id}")]
        public async Task<IActionResult> BorrarBodega(int id)
        {
            var bodega = await _context.Bodegas.FindAsync(id);
            if (bodega == null) return NotFound();

            _context.Bodegas.Remove(bodega);

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

     
        // --- EXISTENCIAS ---
       
        [Tags("Existencias")]
        [HttpGet("existencias")]
        public async Task<ActionResult<IEnumerable<Existencia>>> GetExistencias() => await _context.Existencias.Include(e => e.Bodega).AsNoTracking().ToListAsync();

        [Tags("Existencias")]
        [HttpGet("existencias/{id}")]
        public async Task<ActionResult<Existencia>> GetExistencia(int id)
        {
            var existencia = await _context.Existencias.Include(e => e.Bodega).FirstOrDefaultAsync(e => e.IdExistencia == id);
            return existencia == null ? NotFound() : existencia;
        }

        [Tags("Existencias")]
        [HttpPost("existencias")]
        public async Task<ActionResult<Existencia>> CrearExistencia(Existencia existencia)
        {
            _context.Existencias.Add(existencia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetExistencia), new { id = existencia.IdExistencia }, existencia);
        }

        [Tags("Existencias")]
        [HttpPut("existencias/{id}")]
        public async Task<IActionResult> ActualizarExistencia(int id, Existencia existencia)
        {
            if (id != existencia.IdExistencia) return BadRequest();
            _context.Entry(existencia).State = EntityState.Modified;

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

        [Tags("Existencias")]
        [HttpDelete("existencias/{id}")]
        public async Task<IActionResult> BorrarExistencia(int id)
        {
            var existencia = await _context.Existencias.FindAsync(id);
            if (existencia == null) return NotFound();

            _context.Existencias.Remove(existencia);

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

        // --- MOVIMIENTOS ---
       
        [Tags("Movimientos")]
        [HttpGet("movimientos")]
        public async Task<ActionResult<IEnumerable<MovimientoInv>>> GetMovimientos() => await _context.Movimientos.Include(m => m.Bodega).AsNoTracking().ToListAsync();

        [Tags("Movimientos")]
        [HttpGet("movimientos/{id}")]
        public async Task<ActionResult<MovimientoInv>> GetMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.Include(m => m.Bodega).FirstOrDefaultAsync(m => m.IdMovimientoInv == id);
            return movimiento == null ? NotFound() : movimiento;
        }

        [Tags("Movimientos")]
        [HttpPost("movimientos")]
        public async Task<ActionResult<MovimientoInv>> CrearMovimiento(MovimientoInv movimiento)
        {
            _context.Movimientos.Add(movimiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetMovimiento), new { id = movimiento.IdMovimientoInv }, movimiento);
        }

        [Tags("Movimientos")]
        [HttpPut("movimientos/{id}")]
        public async Task<IActionResult> ActualizarMovimiento(int id, MovimientoInv movimiento)
        {
            if (id != movimiento.IdMovimientoInv) return BadRequest();
            _context.Entry(movimiento).State = EntityState.Modified;

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

        [Tags("Movimientos")]
        [HttpDelete("movimientos/{id}")]
        public async Task<IActionResult> BorrarMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null) return NotFound();

            _context.Movimientos.Remove(movimiento);

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

      
        // --- RESERVAS ---
      
        [Tags("Reservas")]
        [HttpGet("reservas")]
        public async Task<ActionResult<IEnumerable<ReservaStock>>> GetReservas() => await _context.Reservas.Include(r => r.Bodega).AsNoTracking().ToListAsync();

        [Tags("Reservas")]
        [HttpGet("reservas/{id}")]
        public async Task<ActionResult<ReservaStock>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.Include(r => r.Bodega).FirstOrDefaultAsync(r => r.IdReservaStock == id);
            return reserva == null ? NotFound() : reserva;
        }

        [Tags("Reservas")]
        [HttpPost("reservas")]
        public async Task<ActionResult<ReservaStock>> CrearReserva(ReservaStock reserva)
        {
            _context.Reservas.Add(reserva);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetReserva), new { id = reserva.IdReservaStock }, reserva);
        }

        [Tags("Reservas")]
        [HttpPut("reservas/{id}")]
        public async Task<IActionResult> ActualizarReserva(int id, ReservaStock reserva)
        {
            if (id != reserva.IdReservaStock) return BadRequest();
            _context.Entry(reserva).State = EntityState.Modified;

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

        [Tags("Reservas")]
        [HttpDelete("reservas/{id}")]
        public async Task<IActionResult> BorrarReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();

            _context.Reservas.Remove(reserva);

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