using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pi.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetSecureData")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetSecureData()
        {
            return Ok("Online Admin view");
        }
    }
}
