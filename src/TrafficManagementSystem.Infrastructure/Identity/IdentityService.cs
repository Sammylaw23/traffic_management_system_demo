using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TrafficManagementSystem.Application.DTOs.User;
using TrafficManagementSystem.Application.Exceptions;
using TrafficManagementSystem.Application.Interfaces.Services;
using TrafficManagementSystem.Application.Wrappers;
using TrafficManagementSystem.Domain.Settings;
using TrafficManagementSystem.Infrastructure.Models;

namespace TrafficManagementSystem.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(IOptions<JWTSettings> jwtsettings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _jwtSettings = jwtsettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<Response<AuthenticationResponse>> LoginAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new ApiException("Invalid credentials.");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            //var result = await _signInManager.CheckPasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
                throw new ApiException("Invalid credentials.");

            if (!user.EmailConfirmed)
                throw new ApiException($"Account not confirmed for '{request.Email}'.");

            //if (!user.Active)
            //    throw new ApiException("User account is inactive.");
           
            var response = new AuthenticationResponse()
            {
                Email = user.Email,
                //DisplayName = user.DisplayName,

            };
            response.JWToken = GenerateToken(user);

            return new Response<AuthenticationResponse>(response);
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Surname, user!.LastName!),
                new Claim(ClaimTypes.GivenName, user!.FirstName!),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.UserName),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                //issuer: _jwtSettings.Issuer,
                //audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

      

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
