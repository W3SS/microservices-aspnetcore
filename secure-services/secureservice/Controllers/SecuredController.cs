using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Secureservice.Controllers
{
    [Route("/api/secured")]
    public class SecuredController : Controller
    {
        [Authorize]
        [HttpGet]
        public string Get() 
        {
            foreach (var claim in this.HttpContext.User.Claims) {
                Console.WriteLine($"{claim.Type}:{claim.Value}");
            }
            return "This is from the super secret area";
        }

        [Authorize( Policy = "CheeseburgerPolicy")]
        [HttpGet("policy")]
        public string GetWithPolicy()
        {
            return "This is from the super secret area w/policy enforcement.";
        }
    }
}