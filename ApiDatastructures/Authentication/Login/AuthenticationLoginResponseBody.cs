
namespace AnonKey_Backend.ApiDatastructures.Authentication.Login;

public class AuthenticationLoginResponseBody
{
    public string Token { get; set; }
    public int ExpiresInSeconds { get; set; }
}

