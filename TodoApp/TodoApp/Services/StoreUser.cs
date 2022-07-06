using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TodoApp.Data;
using TodoApp.Models.DTOs;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services {
    public class StoreUser : IUserStore<UserDTO>, IUserEmailStore<UserDTO>, IUserPasswordStore<UserDTO> {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public StoreUser(
            IUserRepository userRepository,
            IMapper mapper
        ) {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateAsync(UserDTO user, CancellationToken cancellationToken) {
            var userStorage = _mapper.Map<User>(user);
            user.Id = await _userRepository.CreateUserAsync(userStorage);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(UserDTO user, CancellationToken cancellationToken) {
            var userStorage = _mapper.Map<User>(user);
            await _userRepository.DeleteUserAsync(userStorage);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(UserDTO user, CancellationToken cancellationToken) {
            var userStorage = _mapper.Map<User>(user);
            await _userRepository.UpdateUserAsync(userStorage);
            return IdentityResult.Success;
        }

        public async Task<UserDTO> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken) {
            var user = await _userRepository.GetByEmailAsync(normalizedEmail);
            var userStorage = _mapper.Map<UserDTO>(user);
            return userStorage;
        }

        public async Task<UserDTO> FindByIdAsync(string userId, CancellationToken cancellationToken) {
            var user = await _userRepository.GetByIdAsync(int.Parse(userId));
            var userStorage = _mapper.Map<UserDTO>(user);
            return userStorage;
        }

        public async Task<UserDTO> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
            var user = await _userRepository.GetByUserNameAsync(normalizedUserName);
            var userStorage = _mapper.Map<UserDTO>(user);
            return userStorage;
        }

        public Task<string> GetEmailAsync(UserDTO user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(UserDTO user, CancellationToken cancellationToken) {
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(UserDTO user, CancellationToken cancellationToken) {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(UserDTO user, CancellationToken cancellationToken) {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(UserDTO user, CancellationToken cancellationToken) {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(UserDTO user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(UserDTO user, CancellationToken cancellationToken) {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(UserDTO user, CancellationToken cancellationToken) {
            return Task.FromResult(true);
        }

        public Task SetEmailAsync(UserDTO user, string email, CancellationToken cancellationToken) {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(UserDTO user, bool confirmed, CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(UserDTO user, string normalizedEmail, CancellationToken cancellationToken) {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(UserDTO user, string normalizedName, CancellationToken cancellationToken) {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(UserDTO user, string passwordHash, CancellationToken cancellationToken) {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(UserDTO user, string userName, CancellationToken cancellationToken) {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public void Dispose() { }
    }
}