using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SecureWebApp.Controllers
{
    using Microsoft.AspNetCore.Authentication;

    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl = "/")
        {
            return new ChallengeResult("Auth0", new AuthenticationProperties { RedirectUri = returnUrl });
        }

        [Authorize]
        public IActionResult Logout()
        {
            this.HttpContext.Authentication.SignOutAsync("Auth0");
            this.HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Claims()
        {
            this.ViewData["Title"] = "Claims";
            var identity = this.HttpContext.User.Identity as ClaimsIdentity;
            this.ViewData["picture"] = identity?.FindFirst("picture").Value;
            return this.View();
        }
    }
}