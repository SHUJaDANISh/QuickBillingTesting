using Microsoft.AspNetCore.Mvc;

namespace QuickBillingTesting.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get() => "API is running";
    }

}
