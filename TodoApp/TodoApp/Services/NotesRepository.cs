using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Enums;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class NotesRepository : INotesRepository {
        private readonly TodoAppContext _context;

        public NotesRepository(TodoAppContext context) {
            _context = context;
        }

        public async Task<Note> Get(int id) {
            return await _context.Notes
                .Where(n => n.Id == id)
                .Include(n => n.User)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Complete(Note note) {
            try {
                var noteSearched = await SearchNote(note.Id);
                noteSearched.State = (int)StateTask.Terminada;

                _context.Entry(noteSearched).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> UnComplete(Note note) {
            try {
                var noteSearched = await SearchNote(note.Id);
                noteSearched.State = (int)StateTask.Activa;

                _context.Entry(noteSearched).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        private async Task<Note> SearchNote(int id) {
            return await _context.Notes
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}