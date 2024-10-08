namespace AnonKey_Backend.ApiDatastructures.Users.Update;

/// <summary>
/// User update request user.
/// </summary>
public class UsersUpdateUser
{
    /// <summary>
    /// The username of the user to be updated.
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// The new display name of the user.
    /// </summary>
    public string? DisplayName { set; get; }
}

