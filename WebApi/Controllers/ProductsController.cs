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
    /// <summary>
    /// Products Controller Class
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService">Product Service Interface</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all products in the system
        /// </summary>
        /// <returns>Action Result of a list of Products</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "All products were retrieved successfully")]
        public async Task<ActionResult<List<ProductVM>>> GetAllProducts()
        {
            return await _productService.GetAllAsync();
        }

        /// <summary>
        /// Get all active products in the system
        /// </summary>
        /// <returns>Action Result of a list of active Products</returns>
        [AllowAnonymous]
        [HttpGet("Active")]
        [SwaggerResponse(StatusCodes.Status200OK, "The active products were retrieved successfully")]
        public async Task<ActionResult<List<ProductVM>>> GetActiveProducts()
        {
            return await _productService.GetAllActiveAsync();
        }

        /// <summary>
        /// Get products by category identifier
        /// </summary>
        /// <param name="categoryId">Category System Identifier (1 or 2)</param>
        /// <returns>Action Result of a list of Products of a particular Category</returns>
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

        /// <summary>
        /// Get a product by system identifier
        /// </summary>
        /// <param name="id">Product System Identifier</param>
        /// <returns>Action Result of a particular Product</returns>
        [AllowAnonymous]
        [ResponseCache(Duration = 1800)]
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

        /// <summary>
        /// Create a product in the system
        /// </summary>
        /// <param name="product">Product info to be created</param>
        /// <returns>Action Result of the created Product with identifier generated</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The product was created successfully")]
        public async Task<ActionResult<ProductVM>> AddProduct(ProductForUpdateVM product)
        {
            await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        /// <summary>
        /// Update a product by identifier in the system
        /// </summary>
        /// <param name="id">System identifier of the Product to be updated</param>
        /// <param name="product">Product info to be updated</param>
        /// <returns>No Content Result if updated successfully</returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was updated successfully", typeof(NoContentResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The product information does not match", typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateProduct(int id, ProductForUpdateVM product)
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

        /// <summary>
        /// Activate a product by identifier in the system
        /// </summary>
        /// <param name="id">System identifier of the Product to be activated</param>
        /// <returns>No Content Result if activated successfully</returns>
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

        /// <summary>
        /// Soft-delete a product by identifier from the system
        /// </summary>
        /// <param name="id">System identifier of the Product to be deleted</param>
        /// <returns>No Content Result if deleted successfully</returns>
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
