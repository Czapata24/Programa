using HankoSpa.DTOs;
using HankoSpa.Nucleo;
using HankoSpa.Helpers; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HankoSpa.Services.Interfaces
{
    public interface IServicioServices
    {
        Task<Response<List<ServiceDTO>>> GetAllAsync();
        Task<Response<ServiceDTO>> GetOneAsync(int id);
        Task<Response<ServiceDTO>> CreateAsync(ServiceDTO dto);
        Task<Response<ServiceDTO>> EditAsync(ServiceDTO dto);
        Task<Response<object>> DeleteAsync(int id);

    }
}