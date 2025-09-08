using Microsoft.AspNetCore.Mvc;

namespace EcoSwap.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // TODO: Replace with real login logic
            TempData["Msg"] = $"Logged in as {email} (demo only)";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Signup() => View();

        [HttpPost]
        public IActionResult Signup(string name, string email, string password)
        {
            // TODO: Replace with real signup logic
            TempData["Msg"] = $"Account created for {name} (demo only)";
            return RedirectToAction("Signup");
        }
    }
}
