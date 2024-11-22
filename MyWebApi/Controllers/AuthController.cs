using Microsoft.AspNetCore.Mvc;
using MyWebApi.Security;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Mock user authentication
            if (login.Username == "admin" && login.Password == "password")
            {
                var token = _tokenService.GenerateToken(login.Username, "Admin");
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}
