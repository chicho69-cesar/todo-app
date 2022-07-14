using TodoApp.Data;

namespace TodoApp.Services.Interfaces {
    public interface INotesRepository {
        Task<bool> Complete(Note note);
        Task<bool> Delete(Note note);
        Task<Note> Get(int id);
        Task<bool> UnComplete(Note note);
    }
}