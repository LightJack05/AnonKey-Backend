namespace AnonKeyBackend.ApiDatastructures.Authentication.RefreshTokens;

/// <summary>
/// The body of the response to a Refresh request.
/// </summary>
public class AuthenticationRefreshTokensResponseBody
{
    /// <summary>
    /// The token for accessing API endpoints
    /// </summary>
    public AuthenticationRefreshTokens? AccessToken { get; set; }
    /// <summary>
    /// The token for refreshing the access token or the refresh token itself
    /// </summary>
    public AuthenticationRefreshTokens? RefreshToken { get; set; }
}
