using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestApi.Models;

namespace TestApi.Controllers
{

    [ApiController]
    [Route("product")]
    public class ProductoesController : ControllerBase
    {
        private readonly TestDbContext _context;

        public ProductoesController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: product
        public ActionResult<List<Producto>> Get()
        {
            var pro = from u in _context.Productos
                       select u;
            return Ok(pro);
        }

        [HttpPost]
        // POST: product
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {

            
             var pro = await _context.Productos.FindAsync(producto.IdProd);
            if(pro == null) { 
                _context.Productos.Add(producto);
                _context.SaveChanges();

                return Ok(producto);
      
            }
            return BadRequest();
        }

        [HttpDelete("BorrarProducto/{id}")]
        // DELETE: product/5
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // product/5
        [HttpPut("EditProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Producto producto)
        {
            var existingProduct = await _context.Productos.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.NombreProd = producto.NombreProd;
            existingProduct.PrecioProd = producto.PrecioProd;
            existingProduct.EstadoProd = producto.EstadoProd;

            _context.Productos.Update(existingProduct);
            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }



    }
}