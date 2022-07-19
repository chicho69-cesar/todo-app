using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Models;
using TodoApp.Models.DTOs;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IMapper _mapper;

        public HomeController(
            ILogger<HomeController> logger,
            IUserService userService,
            IUserRepository userRepository,
            IGroupsRepository groupsRepository,
            IMapper mapper
        ) {
            _logger = logger;
            _userService = userService;
            _userRepository = userRepository;
            _groupsRepository = groupsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string charge = "Notes") {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);
            var groups = await _groupsRepository.GetGroups(userId);

            var model = new GroupsNotesViewModel {
                Charge = charge,
                Notes = user.Notes
                    .Where(n => n.State != 3)
                    .OrderBy(n => n.State)
                    .ThenByDescending(n => n.Date),
                UserGroups = user.UserGroups,
                Groups = groups
                    .Select(g => _mapper.Map<GroupDTO>(g))
                    .ToList()
            };

            ViewBag.User = user.UserName;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotAuthorized() {
            return View();
        }

        [Route("error/404")]
        [AllowAnonymous]
        public IActionResult Error404() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}