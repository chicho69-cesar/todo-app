using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers {
    public class GroupsController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}