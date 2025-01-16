namespace AnonKeyBackend.ApiDatastructures.Users.Create;

/// <summary>
/// Body of a response to a user create request.
/// </summary>
public class UsersCreateResponseBody
{
    /// <summary>
    /// The token for accessing API endpoints
    /// </summary>
    public Create.UsersCreateResponseToken? AccessToken { get; set; }
    /// <summary>
    /// The token for refreshing the access token or the refresh token itself
    /// </summary>
    public Create.UsersCreateResponseToken? RefreshToken { get; set; }
}
