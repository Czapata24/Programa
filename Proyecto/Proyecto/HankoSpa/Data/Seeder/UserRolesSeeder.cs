using HankoSpa.Core;
using HankoSpa.Models;
using HankoSpa.Services;
using HankoSpa.Services.Interfaces;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace HankoSpa.Data.Seeder
{
    public class UserRolesSeeder
    {
        private readonly AppDbContext _context;
        private readonly IUsersService _userServices;

        public UserRolesSeeder(AppDbContext context, IUsersService usersService)
        {
            _context = context;
            _userServices = usersService;
        }

        public async Task SeedAsync()
        {
            await CheckRoles();
            await CheckUsers();
        }

        private async Task CheckUsers()
        {
            //Admin
            User? user = await _userServices.GetUserAsync("cristian@gmail.com");

            if (user is null)
            {
               CustomRol customRol = await _context.CustomRoles.FirstOrDefaultAsync(c => c.NombreRol == Env.SUPERADMINROLENAME);
                
                user = new User
                {
                    Email = "cristian@gmail.com",
                    FirstName = "Cristian",
                    LastName = "Zapata",
                    PhoneNumber = "3012025",
                    UserName = "cristian@gmail.com",
                    Document = "12121",
                    Name = "Daniel",
                    CustomRol = customRol,
                    Citas = null
                };
                 var re = await _userServices.AddUserAsync(user, "123456");
                
                string token = await _userServices.GenerateEmailConfirmationTokenAsync(user);
                await _userServices.ConfirmEmailAsync(user, token);
            }
        }

        private async Task CheckRoles()
        {
            await AdminRoleAsync();
            await ContentManagerRoleAsync();
        }

        private async Task ContentManagerRoleAsync()
        {
            bool exists = await _context.CustomRoles.AnyAsync(c => c.NombreRol == "gestor");

            if (!exists)
            {
                CustomRol role = new CustomRol { NombreRol = "Gestor" };
                await _context.CustomRoles.AddAsync(role);

                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Secciones").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolPermissions.AddAsync(new RolPermission { Permission = permission, Name = role, Role="UX/UI" });
                }

                await _context.SaveChangesAsync();
            }

        }

        private async Task AdminRoleAsync()
        {
            bool exists = await _context.CustomRoles.AnyAsync(c => c.NombreRol == Env.SUPERADMINROLENAME);

            if (!exists)
            {
                CustomRol role = new CustomRol { NombreRol = Env.SUPERADMINROLENAME};
                await _context.CustomRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
    } }
