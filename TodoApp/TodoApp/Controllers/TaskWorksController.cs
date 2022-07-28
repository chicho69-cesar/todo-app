using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Helpers.Interfaces;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class TaskWorksController : Controller {
        private readonly ITasksRepository _tasksRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ISelectListHelper _selectListHelper;

        public TaskWorksController(
            ITasksRepository tasksRepository,
            IGroupsRepository groupsRepository,
            IUserService userService,
            IUserRepository userRepository,
            ISelectListHelper selectListHelper
        ) {
            _tasksRepository = tasksRepository;
            _groupsRepository = groupsRepository;
            _userService = userService;
            _userRepository = userRepository;
            _selectListHelper = selectListHelper;
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
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public async Task<IActionResult> Edit(int taskId, int groupId) {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            if (user is null) {
                return NotFound();
            }

            var task = await _tasksRepository.GetTask(taskId);
            var group = await _groupsRepository.GetGroup(groupId);

            if (task is null || group is null) {
                return NotFound();
            }

            if (!await _groupsRepository.IsUserInGroup(group.Id, user)) {
                return NotFound();
            }

            var model = new EditTaskViewModel {
                Id = task.Id,
                GroupId = group.Id,
                Text = task.Text,
                State = task.State,
                States = _selectListHelper.GetStates(),
                FinishDate = task.EndDate
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTaskViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "La informacion que proporcionaste no es correcta");
                return View(model);
            }

            var task = await _tasksRepository.GetTask(model.Id);
            var group = await _groupsRepository.GetGroup(model.GroupId);

            if (task is null || group is null) {
                return NotFound();
            }

            task.Text = model.Text;
            task.State = model.State;
            task.EndDate = model.FinishDate;

            bool band = await _tasksRepository.EditTask(task);

            if (band) {
                return RedirectToAction("Details", "Groups", new { groupId = group.Id });
            } else {
                ModelState.AddModelError(string.Empty, "Ocurrio un error al modificar la informacion de la tarea");
                return View(model);
            }
        }
    }
}