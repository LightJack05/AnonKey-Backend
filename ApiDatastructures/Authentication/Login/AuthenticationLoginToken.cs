namespace AnonKeyBackend.ApiDatastructures.Authentication.Login;

/// <summary>
/// Represents a token object in the response to a Login request
/// </summary>
public class AuthenticationLoginToken
{
    /// <summary>
    /// The token that can be used for authentication.
    /// </summary>
    public string? Token { get; set; }
    /// <summary>
    /// The type of the token, either "AccessToken" or "RefreshToken"
    /// </summary>
    public string? TokenType { get; set; }
    /// <summary>
    /// The time in seconds the token expires on.
    /// </summary>
    public long ExpiryTimestamp { get; set; }
}
