using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly StoreContext _context;

        public ImagesController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var item = await _context.Images.FindAsync(id);
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
            _context.Images.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImage), new { id = item.Id }, item);
        }
    }
}
