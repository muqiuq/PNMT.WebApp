using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PNMT.WebApp.Authentification;
using System.Security.Claims;

namespace PNMT.WebApp.Controllers
{
    [Controller]
    [Route("/account")]
    public class LoginController : Controller
    {
        private readonly ILogger logger;

        public LoginController(ILogger<LoginController> logger) {
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromServices] WebAppUserManager userManager, [FromForm] string username, [FromForm] string password)
        {
            if (username == null || password == null)
            {
                return NotFound();
            }
            if (!userManager.CheckCredentials(username, password))
            {
                logger.LogInformation($"Loggin failed for {username} from {HttpContext.Connection.RemoteIpAddress}");
                return new RedirectResult("/login?msg=invalid");
            }
            logger.LogInformation($"Loggin successfull for {username} from {HttpContext.Connection.RemoteIpAddress}");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "User")
        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return new RedirectResult("/");
        }

        [AllowAnonymous]
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return new RedirectResult("/login");
        }
    }
}
