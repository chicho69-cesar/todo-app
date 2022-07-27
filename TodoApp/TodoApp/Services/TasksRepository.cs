using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class TasksRepository : ITasksRepository  {
        private readonly TodoAppContext _context;

        public TasksRepository(
            TodoAppContext context
        ) {
            _context = context;
        }

        public async Task<IEnumerable<TaskWork>> GetAllTasks(int groupId) {
            return await _context.TaskWorks
                .Where(t => t.GroupId == groupId)
                .Include(t => t.Group)
                .ToListAsync();
        }

        public async Task<bool> AddTask(TaskWork task) {
            try {
                await _context.TaskWorks.AddAsync(task);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}