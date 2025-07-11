using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G01.APIs.Errors;

namespace Store.G01.APIs.Controllers
{
    [Route("api/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new APIErrorResponse(StatusCodes.Status404NotFound, "Not Found End Point ! "));
        }
    }
}
