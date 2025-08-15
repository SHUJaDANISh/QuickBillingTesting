using Microsoft.AspNetCore.Mvc;

namespace QuickBillingTesting.Controllers
{
    [ApiController]
    [Route("healthz")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Healthy");
    }

}
