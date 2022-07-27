using Microsoft.AspNetCore.Mvc;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class TaskWorksController : Controller {
        private readonly ITasksRepository _tasksRepository;

        public TaskWorksController(
            ITasksRepository tasksRepository
        ) {
            _tasksRepository = tasksRepository;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }
    }
}