using AutoMapper;
using TrafficManagementSystem.Application.DTOs.Vehicle;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Application.Mappings
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<NewVehicleRequest, Vehicle>();
            CreateMap<Vehicle, VehicleDto>();
            
                //.ForMember(dst => dst.RegistrationDate, options => options.MapFrom(src => src.RegistrationDate.ToShortDateString))
        }
    }
}
