using Microsoft.AspNetCore.Mvc;

namespace Veles.API.Controllers
{
   [Route("")]
   [ApiController]
   public class HomeController : Controller
   {
      // GET
      [HttpGet]
      public IActionResult Get() => Content("Veles API");
   }
}