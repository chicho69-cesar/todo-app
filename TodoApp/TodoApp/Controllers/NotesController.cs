using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Helpers.Interfaces;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class NotesController : Controller {
        private readonly INotesRepository _notesRepository;
        private readonly IUserService _userService;
        private readonly ISelectListHelper _selectListHelper;
        private readonly IUserRepository _userRepository;

        public NotesController(
            INotesRepository notesRepository,
            IUserService userService,
            ISelectListHelper selectListHelper,
            IUserRepository userRepository
        ) {
            _notesRepository = notesRepository;
            _userService = userService;
            _selectListHelper = selectListHelper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Add() {
            return View(new AddNoteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddNoteViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Ocurrio un error al agregar la nota");
                return View(model);
            }

            var userId = _userService.GetUserById();
            var user = await _userRepository.GetUserAsync(userId);

            var note = new Note {
                Text = model.Text,
                Date = DateTime.Now,
                State = 0,
                UserId = userId,
                User = user
            };

            bool band = await _notesRepository.Add(note);

            if (band) {
                return RedirectToAction("Index", "Home");
            } else {
                ModelState.AddModelError(string.Empty, "Ocurrio un error al agregar la nota");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int noteId) {
            var userId = _userService.GetUserById();
            var note = await _notesRepository.Get(noteId);

            if (note is null || note.User.Id != userId) {
                return NotFound();
            }

            var model = new EditNoteViewModel {
                Id = note.Id,
                Text = note.Text,
                State = note.State,
                States = _selectListHelper.GetStates()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditNoteViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "La nota que especificaste es incorrecta");
                return View(model);
            }

            var userId = _userService.GetUserById();
            var note = await _notesRepository.Get(model.Id);

            if (note is null || note.User.Id != userId) {
                return NotFound();
            }

            note.Text = model.Text;
            note.State = model.State;

            var band = await _notesRepository.Edit(note);

            return band ? RedirectToAction("Index", "Home") : NotFound();
        }

        [HttpPut]
        [Route("notes/complete/{id}")]
        public async Task<IActionResult> Complete(int id) {
            var userId = _userService.GetUserById();
            var note = await _notesRepository.Get(id);

            if (note is null || note.User.Id != userId) {
                return BadRequest();
            }

            if (note.State == 1) {
                return (await _notesRepository.UnComplete(note))
                    ? Ok() : BadRequest();
            }

            return (await _notesRepository.Complete(note))
                ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("notes/delete/{id}")]
        public async Task<IActionResult> Delete(int id) {
            var userId = _userService.GetUserById();
            var note = await _notesRepository.Get(id);

            if (note is null || note.User.Id != userId) {
                return BadRequest();
            }

            return (await _notesRepository.Delete(note))
                ? Ok() : BadRequest();
        }
    }
}