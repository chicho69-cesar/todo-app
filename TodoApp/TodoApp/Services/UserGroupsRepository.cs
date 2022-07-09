using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class UserGroupsRepository : IUserGroupsRepository {
        private readonly TodoAppContext _context;

        public UserGroupsRepository(
            TodoAppContext context
        ) {
            _context = context;
        }

        public async Task<IEnumerable<UserGroup>> GetUsersGroupsAsync(int userId) {
            return await _context.UserGroups
                .Where(ug => ug.UserId == userId)
                .ToListAsync();
        }
    }
}