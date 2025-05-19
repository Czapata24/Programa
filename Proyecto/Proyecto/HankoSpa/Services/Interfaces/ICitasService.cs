using HankoSpa.DTOs;
using HankoSpa.Nucleo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HankoSpa.Services.Interfaces
{
    public interface ICitaServices
    {
        Task<Response<List<CitaDTO>>> GetAllAsync();
        Task<Response<CitaDTO>> GetOneAsync(int id);
        Task<Response<CitaDTO>> CreateAsync(CitaDTO dto);
        Task<Response<CitaDTO>> EditAsync(CitaDTO dto);
        Task<Response<object>> DeleteAsync(int id);
    }
}
