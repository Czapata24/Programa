using HankoSpa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HankoSpa.Repository
{
    public interface ICitaRepository
    {
        // Obtener todas las citas
        Task<List<Cita>> GetAllAsync();

        // Obtener una cita por su ID
        Task<Cita?> GetByIdAsync(int id);

        // Agregar una nueva cita
        Task AddAsync(Cita cita);

        // Actualizar una cita existente
        Task UpdateAsync(Cita cita);

        // Eliminar una cita por su ID
        Task DeleteAsync(int id);
    }
}
