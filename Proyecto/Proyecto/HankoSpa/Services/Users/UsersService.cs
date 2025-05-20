using HankoSpa.Core;
using HankoSpa.Data;
using HankoSpa.DTOs;
using HankoSpa.Models;
using HankoSpa.Repository.Users;
using HankoSpa.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ClaimsUser = System.Security.Claims.ClaimsPrincipal;

namespace HankoSpa.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _usersRepository;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public UsersService(IHttpContextAccessor httpContextAccessor, IUserRepository usersRepository, AppDbContext context, UserManager<User>userManager, SignInManager<User>signInManager)
        {
            _usersRepository = usersRepository;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.Include(c => c.CustomRol).FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
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

        public bool CurrentUserIsAuthenticated()
        {
            ClaimsUser? user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity != null && user.Identity.IsAuthenticated;
        }

        public async Task<bool> CurrentUserIsAuthorizedAsync(string permission, string module )
        {
            ClaimsUser? claimsUser = _httpContextAccessor.HttpContext?.User;
            if (claimsUser is null) {
                return false;
            }
            string? userName = claimsUser.Identity!.Name;

            User? user = await GetUserAsync(userName);

            if (user is null) {
                return false;
            }

            if (user.CustomRol.NombreRol == Env.SUPERADMINROLENAME)
                return true;

            return await _context.Permissions.Include(r => r.RolPermissions).AnyAsync(p => (p.Module == module && p.NombrePermiso == permission)
            && p.RolPermissions.Any(rp => rp.CustomRolId == user.CustomRolId));

        }
    }
}
