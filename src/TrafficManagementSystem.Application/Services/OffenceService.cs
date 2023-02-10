using AutoMapper;
using FluentValidation.Results;
using TrafficManagementSystem.Application.DTOs.Offence;
using TrafficManagementSystem.Application.Exceptions;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Application.Interfaces.Services;
using TrafficManagementSystem.Application.Wrappers;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Application.Services
{
    public class OffenceService : IOffenceService
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMapper _mapper;

        public OffenceService(IRepositoryProvider repositoryProvider, IMapper mapper)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
        }

        private async Task<Response<OffenceDto>> UpdateExistingOffence(NewOffenceRequest request)
        {
            var existingOffence = await _repositoryProvider.OffenceRepository.GetOffenceByIdAsync(request.Id);
            var dbOffence = _mapper.Map(request, existingOffence);
            _repositoryProvider.OffenceRepository.UpdateOffence(dbOffence);
            await _repositoryProvider.SaveChangesAsync();
            return new Response<OffenceDto>(_mapper.Map<OffenceDto>(dbOffence));
        }
        public async Task<Response<OffenceDto>> SaveOffenceAsync(NewOffenceRequest request)
        {
            //TODO: Rethink this logic so that the same offence will not be entered twice and so that two 
            //different offences will not be seen as the same offence
            //var offenceType = await _repositoryProvider.OffenceRepository.GetOffenceByIdAsync(request.LicenseNo);
            //var offence = await _repositoryProvider.OffenceRepository.GetOffenceByLicenseNumberAsync(request.LicenseNo); //TODO: This is wrong
            //if (offence != null)
            //    throw new ApiException($"Offence already exist!");

            if (request.Id != null)
                return await UpdateExistingOffence(request);

            var offenceType = await _repositoryProvider.OffenceTypeRepository.GetOffenceTypeByCodeAsync(request.OffenceTypeCode);
            if (offenceType == null)
                throw new NotFoundException($"OffenceTypeCode does not exist!");

            var response = new Response<OffenceDto>();
            //check if license is valid on Drivers table
            var licenseIsValid = await _repositoryProvider.DriverRepository.LicenseIsValid(request.LicenseNo);
            if (!licenseIsValid)
                response.Messages.Add($"License number is invalid or does not exist");

            //check if plateNumber is valid on Vehicles table
            var plateNumberIsValid = await _repositoryProvider.VehicleRepository.PlateNumberIsValid(request.PlateNumber);
            if (!plateNumberIsValid)
                response.Messages.Add($"Plate number is invalid or does not exist");

            var offence = _mapper.Map<Offence>(request);

            if (response.Messages.Count > 0)
            {
                var possibleErrorVariables = $"{nameof(licenseIsValid)} | {nameof(plateNumberIsValid)}";
                var validationFailures = new List<ValidationFailure>();
                foreach (var error in response.Messages)
                    validationFailures.Add(new ValidationFailure(possibleErrorVariables, error));
                throw new ValidationException(validationFailures);
            }

            await _repositoryProvider.OffenceRepository.AddOffenceAsync(offence);
            await _repositoryProvider.SaveChangesAsync();
            return response;


        }

        public async Task DeleteOffenceAsync(Guid id)
        {
            var offence = await _repositoryProvider.OffenceRepository.GetOffenceByIdAsync(id);
            if (offence == null)
                throw new NotFoundException("Offence not found!");
            _repositoryProvider.OffenceRepository.DeleteOffence(offence);
            await _repositoryProvider.SaveChangesAsync();
        }

        public async Task<Response<IEnumerable<OffenceDto>>> GetAllOffences()
        {
            var offence = await _repositoryProvider.OffenceRepository.GetOffencesAsync();
            return new Response<IEnumerable<OffenceDto>>(_mapper.Map<IEnumerable<OffenceDto>>(offence));
        }

        public async Task<Response<OffenceDto>> GetOffenceById(Guid id)
        {
            var offence = await _repositoryProvider.OffenceRepository.GetOffenceByIdAsync(id);
            return offence == null ? throw new NotFoundException() : new Response<OffenceDto>(_mapper.Map<OffenceDto>(offence));
        }


    }
}
