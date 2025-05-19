using HankoSpa.DTOs;
using HankoSpa.Models;
using Microsoft.AspNetCore.Identity;

namespace HankoSpa.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<IdentityResult> AddUserAsync(User user, string password);
        public Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        public Task<string> GenerateEmailConfirmationTokenAsync(User user);
        public Task<User> GetUserAsync(string email);
        public Task<SignInResult> LoginAsync(LoginDTO dto);
        public Task LogoutAsync();
    }
    }
