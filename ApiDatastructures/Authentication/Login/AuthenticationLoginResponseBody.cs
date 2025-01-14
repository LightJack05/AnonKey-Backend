
namespace AnonKeyBackend.ApiDatastructures.Authentication.Login;

/// <summary>
/// The body of the response to a login request.
/// </summary>
public class AuthenticationLoginResponseBody
{
    /// <summary>
    /// The token for accessing API endpoints
    /// </summary>
    public Login.AuthenticationLoginToken? AccessToken {get; set;}
    /// <summary>
    /// The token for refreshing the access token or the refresh token itself
    /// </summary>
    public Login.AuthenticationLoginToken? RefreshToken {get; set;}
}

