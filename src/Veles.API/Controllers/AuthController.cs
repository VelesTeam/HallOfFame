using Microsoft.AspNetCore.Mvc;

namespace Veles.API.Controllers
{
   [ApiController]
   [Route("/api/{controller}")]
   public class AuthController : Controller
   {

      [HttpPost("sign-up")]
      public IActionResult SignUp()
      {
         return NoContent();
      }

      [HttpPost("sign-in")]
      public IActionResult SignIn()
      {
         return Ok();
      }

      [HttpPost("access-token/refresh")]
      public IActionResult RefreshAccessToken()
      {
         return Ok();
      }

      [HttpPost("access-token/revoke")]
      public IActionResult RevokeAccessToken()
      {
         return Ok();
      }

      [HttpPost("refresh-token/revoke")]
      public IActionResult RevokeRefreshToken()
      {
         return Ok();
      }
   }
}