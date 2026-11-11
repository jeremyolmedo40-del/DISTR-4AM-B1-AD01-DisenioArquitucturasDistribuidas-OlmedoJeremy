using Libro.Api.Data;
using Libro.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Libro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly LibroDBContext _context;

        public LibrosController(LibroDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libros>>> GetLibros()
        {
            var libros = await _context.Libros
                        .AsNoTracking()
                        .ToListAsync();
            return Ok(libros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetLibro(int id)
        {
            var libro = await _context.Libros
                        .AsNoTracking()
                        .FirstOrDefaultAsync(l => l.IdLibro == id); 
            if (libro == null) return NotFound();
            return Ok(libro); 
        }

        [HttpPost]
        public async Task<ActionResult<Libros>> CrearLibro(Libros libro)
        {
            
            _context.Libros.Add(libro);

           
            await _context.SaveChangesAsync();

            
            return CreatedAtAction("GetLibro", new { id = libro.IdLibro }, libro);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarLibro(int id, Libros libro)
        {
            if (id != libro.IdLibro) return BadRequest();

            _context.Entry(libro).State = EntityState.Modified; 
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id); 
            if (libro == null) return NotFound();

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}