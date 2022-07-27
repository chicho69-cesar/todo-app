using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class UserGroupsRepository : IUserGroupsRepository {
        private readonly TodoAppContext _context;
        private readonly IGroupsRepository _groupsRepository;

        public UserGroupsRepository(
            TodoAppContext context,
            IGroupsRepository groupsRepository
        ) {
            _context = context;
            _groupsRepository = groupsRepository;
        }

        public async Task<IEnumerable<UserGroup>> GetUsersGroupsAsync(int userId) {
            return await _context.UserGroups
                .Where(ug => ug.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserGroup> GetUserGroupAsync(int userId, int groupId) {
            return await _context.UserGroups
                .Where(ug => ug.UserId == userId && ug.GroupId == groupId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteUserGroupAsync(int userGroupId) {
            try {
                var userGroupSearched = await _context.UserGroups
                    .Where(ug => ug.Id == userGroupId)
                    .Include(ug => ug.Group)
                    .FirstAsync();

                var groupId = userGroupSearched.Group.Id;

                _context.UserGroups.Remove(userGroupSearched);
                await _context.SaveChangesAsync();

                await _groupsRepository.DeleteGroup(groupId);

                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}