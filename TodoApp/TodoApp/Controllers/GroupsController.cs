using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class GroupsController : Controller {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly IMapper _mapper;

        public GroupsController(
            IUserService userService,
            IUserRepository userRepository,
            IGroupsRepository groupsRepository,
            ITasksRepository tasksRepository,
            IMapper mapper
        ) {
            _userService = userService;
            _userRepository = userRepository;
            _groupsRepository = groupsRepository;
            _tasksRepository = tasksRepository;
            _mapper = mapper;
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

        [HttpGet]
        public IActionResult Create() {
            return View(new CreateGroupViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);
            var group = _mapper.Map<Group>(model);

            if ((user is null) || (group is null)) {
                return NotFound();
            }

            var band = await _groupsRepository.Add(group, user);

            if (band) {
                return RedirectToAction(nameof(Details), new { groupId = group.Id });
            } else {
                ModelState.AddModelError(string.Empty, "Ocurrio un error al crear el grupo");
                return View(model);
            }
        }
    }
}