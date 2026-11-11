using Inventario.Data;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public InventariosController(InventarioDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventarios>>> GetInventarios()
        {
            var inventarios = await _context.Inventarios
                        .AsNoTracking()
                        .ToListAsync();
            return Ok(inventarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventarios>> GetInventario(int id)
        {
            var inventario = await _context.Inventarios
                        .AsNoTracking()
                        .FirstOrDefaultAsync(i => i.IdInventario == id);
            if (inventario == null) return NotFound();
            return Ok(inventario);
        }

        [HttpPost]
        public async Task<ActionResult<Inventarios>> CrearInventario(Inventarios inventario)
        {
            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario", new { id = inventario.IdInventario }, inventario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarInventario(int id, Inventarios inventario)
        {
            if (id != inventario.IdInventario) return BadRequest();

            _context.Entry(inventario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarInventario(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null) return NotFound();

            _context.Inventarios.Remove(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}