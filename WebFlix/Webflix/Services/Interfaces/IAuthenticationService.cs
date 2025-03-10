// IAuthenticationService.cs
using System.Threading.Tasks;

namespace Webflix.Services.Interfaces
{
    public interface IAuthenticationService
    {
        // Task<bool> AuthenticateAsync(string username, string password); // Correct method name
        Task<(bool IsAuthenticated, string ErrorMessage)> AuthenticateAsync(string username, string password);
    }
}