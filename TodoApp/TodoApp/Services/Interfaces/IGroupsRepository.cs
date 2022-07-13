using TodoApp.Data;

namespace TodoApp.Services.Interfaces {
    public interface IGroupsRepository {
        Task<IEnumerable<Group>> GetGroups(int userId);
    }
}