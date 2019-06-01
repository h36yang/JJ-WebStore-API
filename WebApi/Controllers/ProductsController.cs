using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerResponse(StatusCodes.Status200OK, "The product was retrieved successfully", typeof(Models.Database.Product))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product ID was not found", typeof(ErrorResponse))]
        public async Task<IActionResult> GetProduct(int id)
        {
            var baseProduct = await _context.Product.FindAsync(id);
            if (baseProduct == null)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"Product ID {id} was not found"));
            }

            List<Models.Database.ProductImageRel> imageRel = await _context.ProductImageRel.Where(x => x.ProductId == id).ToListAsync();
            var product = new Product(baseProduct)
            {
                ProductImageIds = imageRel.Select(r => r.ImageId).ToList()
            };
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The product was created successfully", typeof(Models.Database.Product))]
        public async Task<ActionResult<Models.Database.Product>> AddProduct(Models.Database.Product item)
        {
            _context.Product.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, item);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was updated successfully", typeof(NoContentResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The product information does not match", typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateProduct(int id, Models.Database.Product item)
        {
            if (id != item.Id)
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, $"Product ID {id} does not match ID in Product Data {item.Id}"));
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Products/Activate/5
        [HttpPut("Activate/{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was updated successfully", typeof(NoContentResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product ID was not found", typeof(ErrorResponse))]
        public async Task<IActionResult> ActivateProduct(int id)
        {
            var item = await _context.Product.FindAsync(id);
            if (item == null)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"Product ID {id} was not found"));
            }

            item.IsActive = true;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was updated successfully", typeof(NoContentResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product ID was not found", typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var item = await _context.Product.FindAsync(id);
            if (item == null)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"Product ID {id} was not found"));
            }

            item.IsActive = false;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
