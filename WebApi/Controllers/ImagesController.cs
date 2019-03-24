using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebApi.Models.Database;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly WebStoreContext _context;

        public ImagesController(WebStoreContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await _context.Image.ToListAsync();
        }

        // GET: api/Images/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var item = await _context.Image.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST: api/Images
        [HttpPost]
        public async Task<ActionResult<Image>> AddImage(Image item)
        {
            _context.Image.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImage), new { id = item.Id }, item);
        }

        // GET: api/Images/ByProduct/5
        [AllowAnonymous]
        [HttpGet("ByProduct/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductImageRel>>> GetProductImageRel(int productId)
        {
            return await _context.ProductImageRel.Where(x => x.ProductId == productId).ToListAsync();
        }

        // POST: api/Images/AssignToProduct
        [HttpPost("AssignToProduct")]
        public async Task<ActionResult<IEnumerable<ProductImageRel>>> AssignImageToProduct(ProductImageRel item)
        {
            _context.ProductImageRel.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductImageRel), new { productId = item.ProductId }, item);
        }
    }
}
