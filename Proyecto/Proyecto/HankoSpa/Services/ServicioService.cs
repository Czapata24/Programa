using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HankoSpa.DTOs;
using HankoSpa.Models;
using HankoSpa.Nucleo;
using HankoSpa.Repository;
using HankoSpa.Services.Interfaces;

namespace HankoSpa.Services
{
    public class ServicioService : IServicioServices
    {
        private readonly IServicioRepository _repository;
        private readonly IMapper _mapper;

        public ServicioService(IServicioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Obtener todos los servicios
        public async Task<Response<List<ServiceDTO>>> GetAllAsync()
        {
            try
            {
                var servicios = await _repository.GetAllAsync();
                var dtoList = _mapper.Map<List<ServiceDTO>>(servicios);
                return new Response<List<ServiceDTO>>(true, "Servicios obtenidos correctamente.", dtoList);
            }
            catch (Exception ex)
            {
                return HandleException<List<ServiceDTO>>(ex, "Error al obtener los servicios.");
            }
        }

        // Obtener un servicio por su ID
        public async Task<Response<ServiceDTO>> GetOneAsync(int id)
        {
            try
            {
                var servicio = await _repository.GetByIdAsync(id);
                if (servicio == null)
                    return new Response<ServiceDTO>(false, "Servicio no encontrado.");

                var dto = _mapper.Map<ServiceDTO>(servicio);
                return new Response<ServiceDTO>(true, "Servicio obtenido correctamente.", dto);
            }
            catch (Exception ex)
            {
                return HandleException<ServiceDTO>(ex, "Error al obtener el servicio.");
            }
        }

        // Crear un nuevo servicio
        public async Task<Response<ServiceDTO>> CreateAsync(ServiceDTO dto)
        {
            try
            {
                var servicio = _mapper.Map<Servicio>(dto);
                await _repository.AddAsync(servicio);

                var resultDto = _mapper.Map<ServiceDTO>(servicio);
                return new Response<ServiceDTO>(true, "Servicio creado correctamente.", resultDto);
            }
            catch (Exception ex)
            {
                return HandleException<ServiceDTO>(ex, "Error al crear el servicio.");
            }
        }

        // Editar un servicio existente
        public async Task<Response<ServiceDTO>> EditAsync(ServiceDTO dto)
        {
            try
            {
                var servicio = await _repository.GetByIdAsync(dto.ServicioId);
                if (servicio == null)
                    return new Response<ServiceDTO>(false, "Servicio no encontrado.");

                // Actualizar propiedades
                servicio.NombreServicio = dto.NombreServicio;
                servicio.DescripcionServicio = dto.DescripcionServicio;

                await _repository.UpdateAsync(servicio);

                var resultDto = _mapper.Map<ServiceDTO>(servicio);
                return new Response<ServiceDTO>(true, "Servicio actualizado correctamente.", resultDto);
            }
            catch (Exception ex)
            {
                return HandleException<ServiceDTO>(ex, "Error al editar el servicio.");
            }
        }

        // Eliminar un servicio por su ID
        public async Task<Response<object>> DeleteAsync(int id)
        {
            try
            {
                var servicio = await _repository.GetByIdAsync(id);
                if (servicio == null)
                    return new Response<object>(false, "Servicio no encontrado.");

                await _repository.DeleteAsync(id);
                return new Response<object>(true, "Servicio eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return HandleException<object>(ex, "Error al eliminar el servicio.");
            }
        }

        // Manejo centralizado de excepciones
        private Response<T> HandleException<T>(Exception ex, string message)
        {
            return new Response<T>(false, message)
            {
                Errors = new List<string> { ex.Message }
            };
        }
    }
}