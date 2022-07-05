using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Data.Entities;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class UsersController : Controller {
        private readonly ILogger _logger;
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IBlobService _blobService;
        private readonly SignInManager<User> _signInManager;

        public UsersController(
            ILogger<UsersController> logger,
            DataContext context,
            IUserService userService,
            IBlobService blobService,
            SignInManager<User> signInManager
        ) {
            _logger = logger;
            _context = context;
            _userService = userService;
            _blobService = blobService;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model) {
            _logger.LogInformation(model.Email);
            
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Tienes un error en los datos");
                return View(model);
            }

            var result = await _userService.LoginAsync(model);

            if (result.Succeeded) {
                _logger.LogInformation("Si llego aqui");
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut) {
                ModelState.AddModelError(string.Empty, "Ha superado el máximo número de intentos, su cuenta está bloqueada, intente de nuevo en 5 minutos.");
            } else {
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() {
            var model = new RegisterViewModel {
                Id = Guid.Empty.ToString()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            Guid imageId = Guid.Empty;

            if (model.ImageFile != null) {
                imageId = await _blobService.UploadBlobAsync(model.ImageFile, "users");
            }

            model.ImageId = imageId;
            var user = await _userService.AddUserAsync(model);

            if (user is null) {
                _logger.LogInformation("Me esta regresando null alv");
                ModelState.AddModelError(string.Empty, "Este correo ya esta siendo usado, o la contraseña es incorrecta");
                return View(model);
            } else {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
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
    }
}