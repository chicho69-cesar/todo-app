using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class GroupsRepository : IGroupsRepository {
        private readonly TodoAppContext _contex;

        public GroupsRepository(TodoAppContext contex) {
            _contex = contex;
        }

        public async Task<IEnumerable<Group>> GetGroups(int userId) {
            var userGroups = await _contex.UserGroups
                .Where(ug => ug.UserId == userId)
                .ToListAsync();

            var groups = userGroups
                .Select(g => g.Group)
                .ToList();

            return groups;
        }

        public async Task<Group> GetGroup(int groupId) {
            return await _contex.Groups
                .Where(g => g.Id == groupId)
                .Include(g => g.TaskWorks)
                .Include(g => g.UserGroups)
                .ThenInclude(ug => ug.User)
                .FirstOrDefaultAsync();
        }
    }
}