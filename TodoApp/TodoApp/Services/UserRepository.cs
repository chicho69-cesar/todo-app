using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class UserRepository : IUserRepository {
        private readonly TodoAppContext _context;

        public UserRepository(TodoAppContext context) {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() {
            return await _context.Users
                .ToListAsync();
        }

        public async Task<User> GetUserAsync(int id) {
            return await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Notes)
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .ThenInclude(g => g.TaskWorks)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateUserAsync(User user) {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var userInStore = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            return userInStore.Id;
        }

        public async Task UpdateUserAsync(User user) {
            var updatedUser = await _context.Users
                .Where(u => u.Id == user.Id)
                .FirstAsync();

            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.Email = user.Email;
            updatedUser.UserName = user.UserName;
            updatedUser.ImageId = user.ImageId;
            updatedUser.PasswordHash = user.PasswordHash;

            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user) {
            var deletedUser = await _context.Users
                .Where(u => u.Id == user.Id)
                .FirstAsync();

            _context.Users.Remove(deletedUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id) {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email) {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.NormalizedEmail == email);
        }

        public async Task<User> GetByUserNameAsync(string username) {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == username);
        }
    }
}