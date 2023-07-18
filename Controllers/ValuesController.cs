using Microsoft.AspNetCore.Mvc;

namespace ServiceProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new[] { "Value 1", "Value 2" });
        }
    }
}
