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
                var band = await Modified(
                    await SearchNote(note.Id),
                    (int)StateTask.Terminada
                );

                return band;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> UnComplete(Note note) {
            try {
                var band = await Modified(
                    await SearchNote(note.Id),
                    (int)StateTask.Activa
                );

                return band;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> Delete(Note note) {
            try {
                var band = await Modified(
                    await SearchNote(note.Id),
                    (int)StateTask.Eliminada
                );

                return band;
            } catch(Exception) {
                return false;
            }
        }

        public async Task<bool> Modified(Note note, int newState) {
            try {
                note.State = newState;
                _context.Entry(note).State = EntityState.Modified;
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