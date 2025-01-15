namespace AnonKeyBackend.ApiDatastructures.Users.Create;

/// <summary>
/// Represents a token object in the response to a user UsersCreateResponseToken request
/// </summary>
public class UsersCreateResponseToken
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
