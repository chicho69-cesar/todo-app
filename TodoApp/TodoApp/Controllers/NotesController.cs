using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers {
    public class NotesController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}