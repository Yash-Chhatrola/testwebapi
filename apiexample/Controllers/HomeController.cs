using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiexample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {

            return Ok("Home get data");
        }
    }
}
