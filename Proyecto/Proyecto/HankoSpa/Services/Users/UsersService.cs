using HankoSpa.Data;
using HankoSpa.DTOs;
using HankoSpa.Models;
using HankoSpa.Repository.Users;
using HankoSpa.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace HankoSpa.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _usersRepository;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        

        public UsersService(IUserRepository usersRepository, AppDbContext context, UserManager<User>userManager, SignInManager<User>signInManager)
        {
            _usersRepository = usersRepository;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user );
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
           return await _userManager.ConfirmEmailAsync(user, token);
        }
        public async Task<SignInResult> LoginAsync(LoginDTO dto)
        {
            return await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
