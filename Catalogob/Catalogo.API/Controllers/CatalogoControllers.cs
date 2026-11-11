using Catalogo.Api.Data;
using Catalogo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly CatalogoDBContext _context;

        public CatalogoController(CatalogoDBContext context)
        {
            _context = context;
        }

        // --- CATEGORIAS ---
       
        [Tags("Categorias")]
        [HttpGet("categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias() => await _context.Categorias.AsNoTracking().ToListAsync();

        [Tags("Categorias")]
        [HttpGet("categorias/{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            return categoria == null ? NotFound() : categoria;
        }

        [Tags("Categorias")]
        [HttpPost("categorias")]
        public async Task<ActionResult<Categoria>> CrearCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.IdCategoria }, categoria);
        }

        [Tags("Categorias")]
        [HttpPut("categorias/{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria) return BadRequest();
            _context.Entry(categoria).State = EntityState.Modified;

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

        [Tags("Categorias")]
        [HttpDelete("categorias/{id}")]
        public async Task<IActionResult> BorrarCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            _context.Categorias.Remove(categoria);

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

       
        // --- MARCAS ---
      
        [Tags("Marcas")]
        [HttpGet("marcas")]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas() => await _context.Marcas.AsNoTracking().ToListAsync();

        [Tags("Marcas")]
        [HttpGet("marcas/{id}")]
        public async Task<ActionResult<Marca>> GetMarca(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            return marca == null ? NotFound() : marca;
        }

        [Tags("Marcas")]
        [HttpPost("marcas")]
        public async Task<ActionResult<Marca>> CrearMarca(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMarca), new { id = marca.IdMarca }, marca);
        }

        [Tags("Marcas")]
        [HttpPut("marcas/{id}")]
        public async Task<IActionResult> ActualizarMarca(int id, Marca marca)
        {
            if (id != marca.IdMarca) return BadRequest();
            _context.Entry(marca).State = EntityState.Modified;

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

        [Tags("Marcas")]
        [HttpDelete("marcas/{id}")]
        public async Task<IActionResult> BorrarMarca(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null) return NotFound();

            _context.Marcas.Remove(marca);

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

    
        // --- PRODUCTOS ---
      
        [Tags("Productos")]
        [HttpGet("productos")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
            => await _context.Productos.Include(p => p.Marca).Include(p => p.Categoria).AsNoTracking().ToListAsync();

        [Tags("Productos")]
        [HttpGet("productos/{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Marca)
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            return producto == null ? NotFound() : producto;
        }

        [Tags("Productos")]
        [HttpPost("productos")]
        public async Task<ActionResult<Producto>> CrearProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, producto);
        }

        [Tags("Productos")]
        [HttpPut("productos/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto) return BadRequest();
            _context.Entry(producto).State = EntityState.Modified;

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

        [Tags("Productos")]
        [HttpDelete("productos/{id}")]
        public async Task<IActionResult> BorrarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            _context.Productos.Remove(producto);

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