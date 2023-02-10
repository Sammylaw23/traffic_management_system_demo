using AutoMapper;
using TrafficManagementSystem.Application.DTOs.OffenceType;
using TrafficManagementSystem.Application.Exceptions;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Application.Interfaces.Services;
using TrafficManagementSystem.Application.Wrappers;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Application.Services
{
    public class OffenceTypeService : IOffenceTypeService
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMapper _mapper;

        public OffenceTypeService(IRepositoryProvider repositoryProvider, IMapper mapper)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
        }
        public async Task SaveOffenceTypeAsync(NewOffenceTypeRequest request)
        {
            if (request.Id != null)
            {
                var existingOffenceType = await _repositoryProvider.OffenceTypeRepository.GetOffenceTypeByIdAsync(request.Id);
                var dbOffenceType = _mapper.Map(request, existingOffenceType);
                _repositoryProvider.OffenceTypeRepository.UpdateOffenceType(dbOffenceType); 
            }
            else
            {
                var offenceType = await _repositoryProvider.OffenceTypeRepository.GetOffenceTypeByCodeAsync(request.Code);
                if (offenceType != null)
                    throw new ApiException($"OffenceType already exist with code: {request.Code}.");
                offenceType = _mapper.Map<OffenceType>(request);
                await _repositoryProvider.OffenceTypeRepository.AddOffenceTypeAsync(offenceType);
            }
            await _repositoryProvider.SaveChangesAsync();

        }
        public async Task DeleteOffenceTypeAsync(Guid id)
        {
            var offenceType = await _repositoryProvider.OffenceTypeRepository.GetOffenceTypeByIdAsync(id);
            if (offenceType == null)
                throw new NotFoundException("OffenceType not found!");
            _repositoryProvider.OffenceTypeRepository.DeleteOffenceType(offenceType);
            await _repositoryProvider.SaveChangesAsync();
        }

        public async Task<Response<IEnumerable<OffenceTypeDto>>> GetAllOffenceTypes()
        {
            var offenceTypes = await _repositoryProvider.OffenceTypeRepository.GetOffenceTypesAsync();
            return new Response<IEnumerable<OffenceTypeDto>>(_mapper.Map<IEnumerable<OffenceTypeDto>>(offenceTypes));
        }

        public async Task<Response<OffenceTypeDto>> GetOffenceTypeById(Guid id)
        {
            var offenceType = await _repositoryProvider.OffenceTypeRepository.GetOffenceTypeByIdAsync(id);
            return offenceType == null ? throw new NotFoundException() : new Response<OffenceTypeDto>(_mapper.Map<OffenceTypeDto>(offenceType));
        }

        public async Task<Response<IEnumerable<string>>> GetOffenceTypeCodes()
        {
            var codes = await _repositoryProvider.OffenceTypeRepository.GetOffenceTypeCodes();
            return new Response<IEnumerable<string>>(codes);
        }
    }
}
