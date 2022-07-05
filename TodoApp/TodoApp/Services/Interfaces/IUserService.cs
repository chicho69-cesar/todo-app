using Microsoft.AspNetCore.Identity;
using TodoApp.Data.Entities;
using TodoApp.Models;

namespace TodoApp.Services.Interfaces {
    public interface IUserService {
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<IdentityResult> AddUserAsync(RegisterViewModel model);
        Task<User> GetUserAsync(string email);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<User> GetUserAsync(Guid userId);
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
        Task<IdentityResult> UpdateUserAsync(User user);
    }
}