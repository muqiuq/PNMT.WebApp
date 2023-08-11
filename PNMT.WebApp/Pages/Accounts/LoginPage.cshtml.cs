using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PNMT.WebApp.Authentification;
using System.Diagnostics;
using System.Security.Claims;

namespace PNMT.WebApp.Pages.Accounts
{
    [AllowAnonymous]
    public class LoginPageModel : PageModel
    {
        public void OnGet()
        {


            Debug.WriteLine("Login");
        }

        public void OnPost()
        {
            var user = HttpContext.User;

            Debug.WriteLine("Login");
        }
    }
}
