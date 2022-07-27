using TodoApp.Data;

namespace TodoApp.Services.Interfaces {
    public interface ITasksRepository {
        Task<bool> AddTask(TaskWork task);
        Task<bool> EditTask(TaskWork task);
        Task<IEnumerable<TaskWork>> GetAllTasks(int groupId);
        Task<TaskWork> GetTask(int taskId);
    }
}