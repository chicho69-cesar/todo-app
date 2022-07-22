using TodoApp.Data;

namespace TodoApp.Services.Interfaces {
    public interface IGroupsRepository {
        Task<Group> GetGroup(int groupId);
        Task<IEnumerable<Group>> GetGroups(int userId);
    }
}