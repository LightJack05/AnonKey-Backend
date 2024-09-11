namespace AnonKey_Backend.ApiDatastructures.Users.Update;

/// <summary>
/// Body of a user update request.
/// </summary>
public class UsersUpdateRequestBody
{
    /// <summary>
    /// User to be updated.
    /// </summary>
    public UsersUpdateUser User { set; get; }
}
