using HankoSpa.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace HankoSpa.Data.Seeder
{
    public class PermissionsSeeder
    {
        private readonly AppDbContext _context;

        public PermissionsSeeder(AppDbContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            List<Permission> permissions = [Section()];

            foreach (PermissionsSeeder permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.NombrePermiso == permission.NombrePermiso && p.Module == permission.Module);

                if (!exists)
                {
                    await _context.Permissions.AddAsync(permission);
                }
            }

            await _context.SaveChangesAsync();

        }

        private List<Permission> Section()
        {
            return new List<Permission>
            {
                new Permission {NombrePermiso = "showSection", Description = "Ver seccion", Module = "Secciones" },
                new Permission {NombrePermiso = "createSection", Description = "Crear seccion", Module = "Secciones" },
                new Permission {NombrePermiso = "updateSection", Description = "Editar seccion", Module = "Secciones" },
                new Permission {NombrePermiso = "deleteSection", Description = "Eliminar seccion", Module = "Secciones" }
            };
        }
    }
}
