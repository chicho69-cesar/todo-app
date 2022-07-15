using Microsoft.AspNetCore.Mvc;
using TodoApp.Helpers.Interfaces;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class NotesController : Controller {
        private readonly INotesRepository _notesRepository;
        private readonly IUserService _userService;
        private readonly ISelectListHelper _selectListHelper;

        public NotesController(
            INotesRepository notesRepository,
            IUserService userService,
            ISelectListHelper selectListHelper
        ) {
            _notesRepository = notesRepository;
            _userService = userService;
            _selectListHelper = selectListHelper;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
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
                States = _selectListHelper.GetNotesStates()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditNoteViewModel model) {
            if (!ModelState.IsValid) {
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