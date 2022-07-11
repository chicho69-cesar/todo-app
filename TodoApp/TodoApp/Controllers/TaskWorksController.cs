using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers {
    public class TaskWorksController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}