namespace AnonKeyBackend.ApiDatastructures.Users.Create;

/// <summary>
/// Body of a user create request.
/// </summary>
public class UsersCreateRequestBody
{
    /// <summary>
    /// Name of the user to be created.
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// Display name of the user to be created.
    /// </summary>
    public string? UserDisplayName { get; set; }
    /// <summary>
    /// Result of the KDF for the user password.
    /// </summary>
    public string? KdfPasswordResult { get; set; }
}
