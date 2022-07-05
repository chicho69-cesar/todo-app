using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Data.Entities;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class UserService : IUserService {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(
            DataContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager
        ) {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password) {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> AddUserAsync(RegisterViewModel model) {
            var user = new User {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageId = model.ImageId
            };

            IdentityResult result = await _userManager.CreateAsync(user, password: model.Password);

            if (result != IdentityResult.Success) {
                return null;
            }

            User newUser = await GetUserAsync(model.Email);

            return newUser;
        }

        public async Task<User> GetUserAsync(string email) {
            return await _context.Users
                .Include(u => u.Notes)
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model) {
            return await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );
        }

        public async Task LogoutAsync() {
            await _signInManager.SignOutAsync();
        }

        public async Task<User> GetUserAsync(Guid userId) {
            return await _context.Users
                .Include(u => u.Notes)
                .Include(u => u.UserGroups)
                .ThenInclude(ug => ug.Group)
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword) {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user) {
            return await _userManager.UpdateAsync(user);
        }
    }
}