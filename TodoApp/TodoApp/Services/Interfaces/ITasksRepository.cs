using TodoApp.Data;

namespace TodoApp.Services.Interfaces {
    public interface ITasksRepository {
        Task<IEnumerable<TaskWork>> GetAllTasks(int groupId);
    }
}