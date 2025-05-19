using HankoSpa.Data;
using HankoSpa.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HankoSpa.Repository
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly AppDbContext _context;

        public ServicioRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los servicios
        public async Task<List<Servicio>> GetAllAsync()
        {
            return await _context.Servicios.ToListAsync();
        }

        // Obtener un servicio por su ID
        public async Task<Servicio?> GetByIdAsync(int id)
        {
            return await _context.Servicios.FirstOrDefaultAsync(s => s.ServicioId == id);
        }

        // Agregar un nuevo servicio
        public async Task AddAsync(Servicio servicio)
        {
            await _context.Servicios.AddAsync(servicio);
            await _context.SaveChangesAsync();
        }

        // Actualizar un servicio existente
        public async Task UpdateAsync(Servicio servicio)
        {
            _context.Servicios.Update(servicio);
            await _context.SaveChangesAsync();
        }

        // Eliminar un servicio por su ID
        public async Task DeleteAsync(int id)
        {
            var servicio = await GetByIdAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }
        }
    }
}