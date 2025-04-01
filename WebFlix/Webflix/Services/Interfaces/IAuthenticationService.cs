// IAuthenticationService.cs
using System.Threading.Tasks;
using Webflix.Models;

namespace Webflix.Services.Interfaces
{
    public interface IAuthenticationService
    {
        // Task<bool> AuthenticateAsync(string username, string password); // Correct method name
        Task<AuthenticationResponse> AuthenticateAsync(string username, string password);
    }
}