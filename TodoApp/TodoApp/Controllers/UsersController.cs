using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Models.DTOs;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class UsersController : Controller {
        private readonly TodoAppContext _context;
        private readonly UserManager<UserDTO> _userManager;
        private readonly SignInManager<UserDTO> _signInManager;
        private readonly IBlobService _blobService;
        private readonly IMapper _mapper;

        public UsersController(
            TodoAppContext context,
            UserManager<UserDTO> userManager,
            SignInManager<UserDTO> signInManager,
            IBlobService blobService,
            IMapper mapper
        ) {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _blobService = blobService;
            _mapper = mapper;
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
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Tienes un error en los datos");
                return View(model);
            }

            var resultado = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (resultado.Succeeded) {
                return RedirectToAction("Index", "Home");
            } else {
                ModelState.AddModelError(string.Empty, "Correo electronico o password incorrecto.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() {
            return View();
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

            var userStorage = _mapper.Map<User>(model);
            var user = _mapper.Map<UserDTO>(userStorage);

            var result = await _userManager.CreateAsync(user, password: model.Password);

            if (result.Succeeded) {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            } else {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
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