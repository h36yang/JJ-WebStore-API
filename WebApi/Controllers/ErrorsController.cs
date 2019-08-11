using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    /// <summary>
    /// Errors Controller Class
    /// </summary>
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        /// <summary>
        /// Get a standard error response based on a HTTP error code
        /// </summary>
        /// <param name="code">HTTP error code, e.g. 404</param>
        /// <returns>Action Result of Standard Error Response</returns>
        [HttpGet("{code}")]
        public ActionResult<ErrorResponse> Error(
            [FromRoute, SwaggerParameter("HTTP Status Code", Required = true)]int code)
        {
            var httpCode = (HttpStatusCode)code;
            var response = new ErrorResponse(code, httpCode.ToString());
            return response;
        }
    }
}
