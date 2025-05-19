using HankoSpa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HankoSpa.Repository
{
    public interface IServicioRepository
    {
        // Obtener todos los servicios
        Task<List<Servicio>> GetAllAsync();

        // Obtener un servicio por su ID
        Task<Servicio?> GetByIdAsync(int id);

        // Agregar un nuevo servicio
        Task AddAsync(Servicio servicio);

        // Actualizar un servicio existente
        Task UpdateAsync(Servicio servicio);

        // Eliminar un servicio por su ID
        Task DeleteAsync(int id);
    }
}