namespace AnonKeyBackend.ApiDatastructures.Authentication.RefreshAccessToken;

/// <summary>
/// The body of the response to a Refresh access token request.
/// </summary>
public class AuthenticationRefreshAccessTokenResponseBody
{
    /// <summary>
    /// The token for accessing API endpoints
    /// </summary>
    public AuthenticationRefreshAccessToken? AccessToken { get; set; }
}
