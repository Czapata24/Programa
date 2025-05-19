using HankoSpa.Data;
using HankoSpa.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HankoSpa.Repository
{
    public class CitaRepository : ICitaRepository
    {
        private readonly AppDbContext _context;

        public CitaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todas las citas
        public async Task<List<Cita>> GetAllAsync()
        {
            return await _context.Citas
                .Include(c => c.Servicio) // Incluye la relación con el servicio
                .ToListAsync();
        }

        // Obtener una cita por su ID
        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _context.Citas
            .Include(c => c.Servicio) // Incluye la relación con el servicio
            .FirstOrDefaultAsync(c => c.CitaId == id);
        }

        // Agregar una nueva cita
        public async Task AddAsync(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
            await _context.SaveChangesAsync();
        }

        // Actualizar una cita existente
        public async Task UpdateAsync(Cita cita)
        {
            _context.Citas.Update(cita);
            await _context.SaveChangesAsync();
        }

        // Eliminar una cita por su ID
        public async Task DeleteAsync(int id)
        {
            var cita = await GetByIdAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();
            }
        }
    }
}
