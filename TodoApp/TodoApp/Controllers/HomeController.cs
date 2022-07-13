using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IGroupsRepository _groupsRepository;

        public HomeController(
            ILogger<HomeController> logger,
            IUserService userService,
            IUserRepository userRepository,
            IGroupsRepository groupsRepository
        ) {
            _logger = logger;
            _userService = userService;
            _userRepository = userRepository;
            _groupsRepository = groupsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string charge = "Notes") {
            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            var model = new GroupsNotesViewModel {
                Charge = charge,
                Notes = user.Notes,
                UserGroups = user.UserGroups,
                Groups = await _groupsRepository.GetGroups(userId)
            };

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