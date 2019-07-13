using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
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
