using Microsoft.AspNetCore.Mvc;
using TodoApp.Services.Interfaces;

namespace TodoApp.Controllers {
    public class NotesController : Controller {
        private readonly INotesRepository _notesRepository;
        private readonly IUserService _userService;

        public NotesController(
            INotesRepository notesRepository,
            IUserService userService
        ) {
            _notesRepository = notesRepository;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
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