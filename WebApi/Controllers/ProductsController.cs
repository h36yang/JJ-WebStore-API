using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/Active
        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<Product>>> GetActiveProducts()
        {
            var items = await _context.Products.ToListAsync();
            return items.Where(i => i.IsActive).ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product item)
        {
            if (!item.IsActive)
            {
                item.IsActive = true;
            }
            _context.Products.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, item);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var item = await _context.Products.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.IsActive = false;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
