using HankoSpa.Core;
using HankoSpa.Models;
using HankoSpa.Services;
using HankoSpa.Services.Interfaces;
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
            User? user = await _userServices.GetUserAsync("Cristian@gmail.com");

            if (user is null)
            {
               CustomRol customRol = await _context.CustomRoles.FirstOrDefaultAsync(c => c.NombreRol == Env.SUPERADMINROLENAME);

                user = new User
                {
                    Email = "Cristian@gmail.com",
                    FirstName = "Cristian",
                    LastName = "Zapata",
                    PhoneNumber = "3012025",
                    UserName = "Cristian@gmail.com",
                    Document = "12121",
                    CustomRol = customRol

                };
                 await _userServices.AddUserAsync(user, "1234");

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
            bool exists = await _context.Servicios.AnyAsync(c => c.NombreServicio == "gestor");

            if (!exists)
            {
                Servicio role = new Servicio { NombreServicio = "Gestor" };
                await _context.Servicios.AddAsync(role);

                List<Permission> permissions = await _context.Permissions.Where(p => p.Module == "Secciones").ToListAsync();

                foreach (Permission permission in permissions)
                {
                    await _context.RolPermissions.AddAsync(new RolPermission { Permission = permission, Role = role });
                }

                await _context.SaveChangesAsync();
            }

        }

        private async Task AdminRoleAsync()
        {
            bool exists = await _context.Servicios.AnyAsync(c => c.NombreServicio == Env.SUPERADMINROLENAME);

            if (!exists)
            {
                Servicio role = new Servicio { NombreServicio = Env.SUPERADMINROLENAME };
                await _context.Servicios.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
    } }
