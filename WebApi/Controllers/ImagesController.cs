using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Database;

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

        // GET: api/Images/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var item = await _context.Image.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return File(item.Data, "image/jpeg");
        }

        // POST: api/Images
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The image was uploaded successfully", typeof(Image))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Missing image name or file data", typeof(ErrorResponse))]
        public async Task<IActionResult> UploadImage(string name, IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, "Must provide a name for the image"));
            }
            else if (file.Length <= 0)
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, "File size is 0"));
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    var fileBytes = stream.ToArray();

                    var imageItem = new Image()
                    {
                        Name = name,
                        Data = fileBytes
                    };
                    _context.Image.Add(imageItem);
                    await _context.SaveChangesAsync();

                    imageItem.Data = null;
                    return CreatedAtAction(nameof(GetImage), new { id = imageItem.Id }, imageItem);
                }
            }
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
