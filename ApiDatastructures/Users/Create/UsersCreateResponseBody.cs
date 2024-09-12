namespace AnonKey_Backend.ApiDatastructures.Users.Create;

/// <summary>
/// Body of a response to a user create request.
/// </summary>
public class UsersCreateResponseBody 
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
