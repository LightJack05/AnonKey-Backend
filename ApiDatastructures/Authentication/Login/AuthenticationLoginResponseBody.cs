
namespace AnonKey_Backend.ApiDatastructures.Authentication.Login;

/// <summary>
/// The body of the response to a login request.
/// </summary>
public class AuthenticationLoginResponseBody
{
    /// <summary>
    /// The token that can be used for authentication.
    /// </summary>
    public string? Token { get; set; }
    /// <summary>
    /// The time in seconds the token expires in.
    /// </summary>
    /// <example>60 Seconds means the token expires in one minute after the request.</example>
    public int ExpiresInSeconds { get; set; }
}

