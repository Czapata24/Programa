using AutoMapper;
using HankoSpa.Models;
using HankoSpa.DTOs;

namespace HankoSpa.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Servicio, ServiceDTO>().ReverseMap();
            CreateMap<Cita, CitaDTO>().ReverseMap();
        }

    }
}