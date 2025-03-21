namespace AnonKeyBackend.ApiDatastructures.Authentication.RefreshRefreshToken;

/// <summary>
/// The body of the response to a Refresh refresh token request.
/// </summary>
public class AuthenticationRefreshRefreshTokenResponseBody
{
    /// <summary>
    /// The token for accessing API endpoints
    /// </summary>
    public AuthenticationRefreshRefreshToken? AccessToken { get; set; }
    /// <summary>
    /// The token for refreshing the access token or the refresh token itself
    /// </summary>
    public AuthenticationRefreshRefreshToken? RefreshToken { get; set; }
}