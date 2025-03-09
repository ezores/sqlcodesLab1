using System;
using System.Threading.Tasks;
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

        // public async Task<(bool IsAuthenticated, string ErrorMessage)> AuthenticateAsync(string username, string password)
        // {
        //     username = username?.Trim().ToLower(); // Normalize casing
        //     password = password?.Trim();
        //
        //     if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //         return (false, "Username or password cannot be empty.");
        //
        //     try
        //     {
        //         bool isClient = await _clientRepository.AuthenticateAsync(username, password);
        //         if (isClient)
        //             return (true, "");
        //
        //         bool isEmployee = await _employeRepository.AuthenticateAsync(username, password);
        //         if (isEmployee)
        //             return (true, "");
        //
        //         return (false, "Invalid credentials. Please try again.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return (false, $"Database error: {ex.Message}");
        //     }
        // }
        public async Task<(bool, string)> AuthenticateAsync(string email, string password)
        {
            email = email?.Trim();
            password = password?.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return (false, "Email and password are required.");

            try
            {
                bool isAuthenticated = await _clientRepository.AuthenticateAsync(email, password);

                if (isAuthenticated)
                {
                    return (true, string.Empty); // Success, no error message
                }
                else
                {
                    return (false, "Invalid credentials. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Authentication failed: {ex.Message}");
                return (false, "An error occurred during authentication.");
            }
        }
    }
}