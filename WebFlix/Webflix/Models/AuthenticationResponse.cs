namespace Webflix.Models;

public class AuthenticationResponse
{
    public bool IsAuthenticated { get; set; }
    public int? ClientId { get; set; }
    public string Message { get; set; } = string.Empty;

    public AuthenticationResponse(bool isAuthenticated, int? clientId = null)
    {
        IsAuthenticated = isAuthenticated;
        ClientId = clientId;
    }
}