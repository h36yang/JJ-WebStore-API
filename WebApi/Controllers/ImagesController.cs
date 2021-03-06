﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using WebApi.Services.Interfaces;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    /// <summary>
    /// Images Controller Class
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="imageService">Image Service Interface</param>
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Get image serialized as jpeg by identifier
        /// </summary>
        /// <param name="id">Image System Identifier</param>
        /// <returns>File Content Result serialized as image/jpeg</returns>
        [AllowAnonymous]
        [ResponseCache(Duration = 31557600)]
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The image was retrieved successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The image ID was not found", typeof(ErrorResponse))]
        public async Task<ActionResult<byte[]>> GetImage(int id)
        {
            var item = await _imageService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound(new ErrorResponse(StatusCodes.Status404NotFound, $"Image ID {id} was not found"));
            }
            return File(item.Data, "image/jpeg");
        }

        /// <summary>
        /// Upload image file with name
        /// </summary>
        /// <param name="name">File Name</param>
        /// <param name="file">Image File as Form Data</param>
        /// <returns>Action Result of uploaded Image with identifier generated</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The image was uploaded successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Missing image name or file data", typeof(ErrorResponse))]
        public async Task<ActionResult<ImageVM>> UploadImage(string name, IFormFile file)
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
                ImageVM imageItem = await _imageService.UploadAsync(name, file);
                return CreatedAtAction(nameof(GetImage), new { id = imageItem.Id }, imageItem);
            }
        }
    }
}
