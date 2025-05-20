using HankoSpa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HankoSpa.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cita> Citas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<CustomRol> CustomRoles { get; set; }
        public DbSet<RolPermission> RolPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Relacion entre Cita y Servicio
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Servicio) // Cita tiene un Servicio
                .WithMany(s => s.Citas) // Servicio tiene muchas Citas
                .HasForeignKey(c => c.ServicioId); // Clave foranea

            // Relacion entre Cita y User
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.User) // Cita tiene un User
                .WithMany(u => u.Citas) // User tiene muchas Citas
                .HasForeignKey(c => c.UsuarioID); // Clave for�nea

            // Relacion entre Rol y User
            modelBuilder.Entity<User>()
                .HasOne(u => u.CustomRol) // User tiene un Rol
                .WithMany(r => r.Usuarios) // Rol tiene muchos Users
                .HasForeignKey(u => u.CustomRolId); // Clave foranea

            // Relacion entre Rol y RolPermission
            modelBuilder.Entity<RolPermission>()
                .HasKey(rp => new { rp.CustomRolId, rp.PermissionId }); // Clave compuesta

            // Relacion entre RolPermission y Rol
            modelBuilder.Entity<RolPermission>()
                .HasOne(rp => rp.Name) // RolPermission tiene un Rol
                .WithMany(r => r.RolPermissions) // Rol tiene muchas RolPermissions
                .HasForeignKey(rp => rp.CustomRolId); // Clave foránea

            // Relacion entre RolPermission y Permission
            modelBuilder.Entity<RolPermission>()
                .HasOne(rp => rp.Permission) // RolPermission tiene un Permission
                .WithMany(p => p.RolPermissions) // Permission tiene muchas RolPermissions
                .HasForeignKey(rp => rp.PermissionId); // Clave foránea

            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}