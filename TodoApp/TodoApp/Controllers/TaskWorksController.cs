using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class TaskWorksController : Controller {
        private readonly ITasksRepository _tasksRepository;
        private readonly IGroupsRepository _groupsRepository;

        public TaskWorksController(
            ITasksRepository tasksRepository,
            IGroupsRepository groupsRepository
        ) {
            _tasksRepository = tasksRepository;
            _groupsRepository = groupsRepository;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int groupId) {
            return View(new AddTaskViewModel {
                GroupId = groupId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Hay un error en la informacion que nos proporcionaste");
                return View(model);
            }

            var group = await _groupsRepository.GetGroup(model.GroupId);

            if (group is null) {
                return NotFound();
            }

            var task = new TaskWork {
                Text = model.Text,
                StartDate = DateTime.Now,
                EndDate = model.FinishDate,
                State = 0,
                GroupId = model.GroupId,
                Group = group
            };

            var band = await _tasksRepository.AddTask(task);

            if (band) {
                return RedirectToAction("Details", "Groups", new { groupId  = model.GroupId });
            } else {
                ModelState.AddModelError(string.Empty, "No fue posible crear esta tarea intentalo de nuevo");
                return View(model);
            }
        }
    }
}