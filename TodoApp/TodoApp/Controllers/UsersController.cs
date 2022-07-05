using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers {
    public class UsersController : Controller {
        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpGet]
        public IActionResult NotAuthorized() {
            return View();
        }
    }
}