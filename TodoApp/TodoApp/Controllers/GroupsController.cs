using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class GroupsController : Controller {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly ITasksRepository _tasksRepository;

        public GroupsController(
            IUserService userService,
            IUserRepository userRepository,
            IGroupsRepository groupsRepository,
            ITasksRepository tasksRepository
        ) {
            _userService = userService;
            _userRepository = userRepository;
            _groupsRepository = groupsRepository;
            _tasksRepository = tasksRepository;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int groupId) {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            if (user == null) {
                return NotFound();
            }

            var group = await _groupsRepository.GetGroup(groupId);
            
            if (!group.UserGroups.Any(g => g.UserId == userId)) {
                return NotFound();
            }

            var model = new GroupTasksViewModel {
                Name = group.Name,
                Tasks = await _tasksRepository.GetAllTasks(groupId)
            };

            return View(model);
        }
    }
}