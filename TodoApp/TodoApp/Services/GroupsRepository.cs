using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Enums;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class GroupsRepository : IGroupsRepository {
        private readonly TodoAppContext _context;

        public GroupsRepository(TodoAppContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetGroups(int userId) {
            var userGroups = await _context.UserGroups
                .Where(ug => ug.UserId == userId)
                .ToListAsync();

            var groups = userGroups
                .Select(g => g.Group)
                .ToList();

            return groups;
        }

        public async Task<Group> GetGroup(int groupId) {
            return await _context.Groups
                .Where(g => g.Id == groupId)
                .Include(g => g.TaskWorks)
                .Include(g => g.UserGroups)
                .ThenInclude(ug => ug.User)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Add(Group group, User user) {
            try {
                var insertedGroup = await _context.Groups.AddAsync(group);
                await _context.SaveChangesAsync();
                
                var searchedUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                var searchedGroup = await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == insertedGroup.Entity.Id);

                await _context.UserGroups.AddAsync(new UserGroup {
                    UserId = searchedUser.Id,
                    User = searchedUser,
                    GroupId = searchedGroup.Id,
                    Group = searchedGroup,
                    State = (int)StateInGroup.Activo
                });

                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> Enter(int groupId, User user) {
            try {
                var searchedUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == user.Id);
                var searchedGroup = await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == groupId);

                await _context.UserGroups.AddAsync(new UserGroup {
                    UserId = searchedUser.Id,
                    User = searchedUser,
                    GroupId = searchedGroup.Id,
                    Group = searchedGroup,
                    State = (int)StateInGroup.Activo
                });

                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> UpdateGroup(Group group) {
            try {
                var updatedGroup = await _context.Groups
                    .Where(g => g.Id == group.Id)
                    .FirstOrDefaultAsync();

                updatedGroup.Name = group.Name;
                updatedGroup.ImageId = group.ImageId;

                _context.Entry(updatedGroup).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> DeleteGroup(int groupId) {
            try {
                var group = await _context.Groups
                    .Where(g => g.Id == groupId)
                    .FirstAsync();

                if (group is not null && group.UserGroups.Count == 0) {
                    _context.Groups.Remove(group);
                    await _context.SaveChangesAsync();
                }

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<bool> IsUserInGroup(int groupId, User user) {
            try {
                var group = await _context.Groups
                    .Where(g => g.Id == groupId)
                    .FirstAsync();

                var isInGroup = group.UserGroups
                    .Any(ug => ug.UserId == user.Id);

                return isInGroup;
            } catch (Exception) {
                return false;
            }
        } 
    }
}