using AutoMapper;
using TrafficManagementSystem.Application.DTOs.Offence;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Application.Mappings
{
    public class OffenceProfile : Profile
    {
        public OffenceProfile()
        {
            CreateMap<NewOffenceRequest, Offence>();
            CreateMap<Offence, OffenceDto>();
        }
    }
}
