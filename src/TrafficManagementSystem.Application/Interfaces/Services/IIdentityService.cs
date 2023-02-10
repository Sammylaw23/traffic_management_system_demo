using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficManagementSystem.Application.DTOs.User;
using TrafficManagementSystem.Application.Wrappers;

namespace TrafficManagementSystem.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<Response<AuthenticationResponse>> LoginAsync(AuthenticationRequest request);

    }
}
