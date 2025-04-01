using System;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Repositories.Interfaces;
using Webflix.Services.Interfaces;

namespace Webflix.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeRepository _employeRepository;

        public AuthenticationService(
            IClientRepository clientRepository,
            IEmployeRepository employeRepository)
        {
            _clientRepository = clientRepository;
            _employeRepository = employeRepository;
        }
        
        public async Task<AuthenticationResponse> AuthenticateAsync(string? email, string? password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new AuthenticationResponse(isAuthenticated: false)
                {
                    Message = "Email and password are required."
                };
            }
                
            email = email.Trim();
            password = password.Trim();

            try
            {
                var response = await _clientRepository.AuthenticateAsync(email, password);

                if (!response.IsAuthenticated)
                {
                    response.Message = "Invalid credentials. Please try again.";
                }
                
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Authentication failed: {ex.Message}");
                return new AuthenticationResponse(isAuthenticated: false)
                {
                    Message = "An error occurred during authentication."
                };
            }
        }
    }
} 