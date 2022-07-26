using TodoApp.Data;

namespace TodoApp.Services.Interfaces {
    public interface IGroupsRepository {
        Task<bool> Add(Group group, User user);
        Task<bool> Enter(int groupId, User user);
        Task<Group> GetGroup(int groupId);
        Task<IEnumerable<Group>> GetGroups(int userId);
        Task<bool> UpdateGroup(Group group);
    }
}