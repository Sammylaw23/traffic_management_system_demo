using AutoMapper;
using TrafficManagementSystem.Application.DTOs.Vehicle;
using TrafficManagementSystem.Application.Exceptions;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Application.Interfaces.Services;
using TrafficManagementSystem.Application.Wrappers;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMapper _mapper;

        public VehicleService(IRepositoryProvider repositoryProvider, IMapper mapper)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
        }
        public async Task SaveVehicleAsync(NewVehicleRequest request)
        {
            if (request.Id != null)
            {
                var existingVehicle = await _repositoryProvider.VehicleRepository.GetVehicleByIdAsync(request.Id);
                var dbVehicle = _mapper.Map(request, existingVehicle);
                _repositoryProvider.VehicleRepository.UpdateVehicle(dbVehicle);
            }
            else
            {
                if (await VehicleExists(request))
                    throw new ApiException("Vehicle already exist.");
                var vehicle = _mapper.Map<Vehicle>(request);
                await _repositoryProvider.VehicleRepository.AddVehicleAsync(vehicle);
            }

            await _repositoryProvider.SaveChangesAsync();
        }

        public async Task DeleteVehicleAsync(Guid id)
        {
            var vehicle = await _repositoryProvider.VehicleRepository.GetVehicleByIdAsync(id);
            if (vehicle == null)
                throw new NotFoundException("Vehicle not found!");
            _repositoryProvider.VehicleRepository.DeleteVehicle(vehicle);
            await _repositoryProvider.SaveChangesAsync();
        }

        public async Task<Response<IEnumerable<VehicleDto>>> GetAllVehicles()
        {
            var vehicles = await _repositoryProvider.VehicleRepository.GetVehiclesAsync();
            return new Response<IEnumerable<VehicleDto>>(_mapper.Map<IEnumerable<VehicleDto>>(vehicles));
        }

        public async Task<Response<VehicleDto>> GetVehicleById(Guid id)
        {
            var vehicle = await _repositoryProvider.VehicleRepository.GetVehicleByIdAsync(id);
            return vehicle == null ? throw new NotFoundException() : new Response<VehicleDto>(_mapper.Map<VehicleDto>(vehicle));
        }

        private async Task<bool> VehicleExists(NewVehicleRequest request)
        {
            var vehicle = _mapper.Map<Vehicle>(request);
            var exist = await _repositoryProvider.VehicleRepository.VehicleExists(vehicle);
            return exist;
        }
    }
}
