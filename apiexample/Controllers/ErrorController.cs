using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiexample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetError()
        {
            var i = 45;
            var j = 0;
            var result = i / j;
            //throw new InvalidOperationException("Something went wrong in the API!");
            return Ok();
        }
    }
}
