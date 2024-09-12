namespace AnonKey_Backend.ApiDatastructures.Users.Get;

/// <summary>
/// Response body of a user get request.
/// </summary>
public class UsersGetResponseBody
{
    /// <summary>
    /// The user requested.
    /// </summary>
    public UsersGetUser? User { set; get; }
}
