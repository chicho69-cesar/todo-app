using TodoApp.Data;

namespace TodoApp.Services.Interfaces {
    public interface ITasksRepository {
        Task<bool> AddTask(TaskWork task);
        Task<bool> Complete(TaskWork task);
        Task<bool> Delete(TaskWork task);
        Task<bool> EditTask(TaskWork task);
        Task<IEnumerable<TaskWork>> GetAllTasks(int groupId);
        Task<TaskWork> GetTask(int taskId);
        Task<bool> UnComplete(TaskWork task);
    }
}