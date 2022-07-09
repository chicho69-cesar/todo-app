using TodoApp.Data;

namespace TodoApp.Services.Interfaces {
    public interface IUserGroupsRepository {
        Task<IEnumerable<UserGroup>> GetUsersGroupsAsync(int userId);
    }
}