using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Models.Database.WebStoreContext _context;

        public ProductsController(Models.Database.WebStoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Database.Product>>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();
        }

        // GET: api/Products/Active
        [AllowAnonymous]
        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<Models.Database.Product>>> GetActiveProducts()
        {
            return await _context.Product.Where(i => i.IsActive.Value).ToListAsync();
        }

        // GET: api/Products/ByCategory
        [AllowAnonymous]
        [HttpGet("ByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Models.Database.Product>>> GetProductsByCategory(int categoryId)
        {
            // Assumption: only return active products
            return await _context.Product.Where(i => i.IsActive.Value && i.CategoryId == categoryId).ToListAsync();
        }

        // GET: api/Products/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Database.Product>> GetProduct(int id)
        {
            Models.Database.Product baseProduct = await _context.Product.FindAsync(id);
            if (baseProduct == null)
            {
                return NotFound();
            }

            List<Models.Database.ProductImageRel> imageRel = await _context.ProductImageRel.Where(x => x.ProductId == id).ToListAsync();
            var product = new Product(baseProduct)
            {
                ProductImageIds = imageRel.Select(r => r.ImageId).ToList()
            };
            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Models.Database.Product>> AddProduct(Models.Database.Product item)
        {
            _context.Product.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, item);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Models.Database.Product item)
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
        public async Task<ActionResult<Models.Database.Product>> DeleteProduct(int id)
        {
            var item = await _context.Product.FindAsync(id);

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
