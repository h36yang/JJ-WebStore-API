using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Services.Interfaces;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "All products were retrieved successfully")]
        public async Task<ActionResult<List<ProductVM>>> GetAllProducts()
        {
            return await _productService.GetAllAsync();
        }

        // GET: api/Products/Active
        [AllowAnonymous]
        [HttpGet("Active")]
        [SwaggerResponse(StatusCodes.Status200OK, "The active products were retrieved successfully")]
        public async Task<ActionResult<List<ProductVM>>> GetActiveProducts()
        {
            return await _productService.GetAllActiveAsync();
        }

        // GET: api/Products/ByCategory
        [AllowAnonymous]
        [ResponseCache(Duration = 1800)]
        [HttpGet("ByCategory/{categoryId}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The products by category were retrieved successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The category ID was not found", typeof(ErrorResponse))]
        public async Task<ActionResult<List<ProductVM>>> GetProductsByCategory(int categoryId)
        {
            List<ProductVM> products = await _productService.GetByCategoryIdAsync(categoryId);
            if (products == null)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"Category ID {categoryId} was not found"));
            }
            return products;
        }

        // GET: api/Products/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The product was retrieved successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product ID was not found", typeof(ErrorResponse))]
        public async Task<ActionResult<ProductVM>> GetProduct(int id)
        {
            ProductVM product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"Product ID {id} was not found"));
            }
            return product;
        }

        // POST: api/Products
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The product was created successfully")]
        public async Task<ActionResult<ProductVM>> AddProduct(ProductVM product)
        {
            await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was updated successfully", typeof(NoContentResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The product information does not match", typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateProduct(int id, ProductVM product)
        {
            if (product == null)
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, $"Product Data is empty or invalid"));
            }
            else if (id != product.Id)
            {
                return BadRequest(new ErrorResponse(StatusCodes.Status400BadRequest, $"Product ID {id} does not match ID in Product Data {product.Id}"));
            }
            await _productService.UpdateAsync(product);
            return NoContent();
        }

        // PUT: api/Products/Activate/5
        [HttpPut("Activate/{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was updated successfully", typeof(NoContentResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product ID was not found", typeof(ErrorResponse))]
        public async Task<IActionResult> ActivateProduct(int id)
        {
            int result = await _productService.ActivateAsync(id);
            if (result == -1)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"Product ID {id} was not found"));
            }
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was updated successfully", typeof(NoContentResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product ID was not found", typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            int result = await _productService.DeleteAsync(id);
            if (result == -1)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"Product ID {id} was not found"));
            }
            return NoContent();
        }
    }
}
