using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Enums;
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

        public async Task<bool> Add(Group group, User user) {
            try {
                var searchedUser = await _contex.Users
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                await _contex.Groups.AddAsync(group);
                await _contex.SaveChangesAsync();

                var searchedGroup = await _contex.Groups
                    .FindAsync(group);

                await _contex.UserGroups.AddAsync(new UserGroup {
                    UserId = searchedUser.Id,
                    User = searchedUser,
                    GroupId = searchedGroup.Id,
                    Group = searchedGroup,
                    State = (int)StateInGroup.Activo
                });

                await _contex.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> Enter(int groupId, User user) {
            try {
                var searchedUser = await _contex.Users
                    .FirstOrDefaultAsync(u => u.Id == user.Id);
                var searchedGroup = await _contex.Groups
                    .FirstOrDefaultAsync(g => g.Id == groupId);

                await _contex.UserGroups.AddAsync(new UserGroup {
                    UserId = searchedUser.Id,
                    User = searchedUser,
                    GroupId = searchedGroup.Id,
                    Group = searchedGroup,
                    State = (int)StateInGroup.Activo
                });

                await _contex.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}