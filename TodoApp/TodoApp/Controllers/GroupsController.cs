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
        private readonly IBlobService _blobService;

        public GroupsController(
            IUserService userService,
            IUserRepository userRepository,
            IGroupsRepository groupsRepository,
            ITasksRepository tasksRepository,
            IMapper mapper,
            IBlobService blobService
        ) {
            _userService = userService;
            _userRepository = userRepository;
            _groupsRepository = groupsRepository;
            _tasksRepository = tasksRepository;
            _mapper = mapper;
            _blobService = blobService;
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
                GroupId = groupId,
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
                ModelState.AddModelError(string.Empty, "Error en la informacion");
                return View(model);
            }

            Guid imageId = Guid.Empty;

            if (model.ImageFile != null) {
                imageId = await _blobService.UploadBlobAsync(model.ImageFile, "groups");
            }

            model.ImageId = imageId;

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

        [HttpGet]
        public IActionResult Enter() {
            return View(new EnterGroupViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Enter(EnterGroupViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Escribe un codigo valido");
                return View(model);
            }

            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            string code = (model.Code).Trim(new char[] { '#' });
            int groupId = int.Parse(code);
            var group = await _groupsRepository.GetGroup(groupId);

            if ((user is null) || (group is null)) {
                return NotFound();
            }

            var band = await _groupsRepository.Enter(groupId, user);

            if (band) {
                return RedirectToAction(nameof(Details), new { groupId });
            } else {
                ModelState.AddModelError(string.Empty, "Codigo incorrecto");
                return View(model);
            }
        }
    }
}