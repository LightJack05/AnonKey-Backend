namespace AnonKey_Backend.ApiDatastructures.Authentication.ChangePassword;

/// <summary>
/// The body of a password change request.
/// </summary>
public class AuthenticationChangePasswordRequestBody
{
    /// <summary>
    /// The KDF result of the old password.
    /// </summary>
    public string? KdfResultOldPassword { get; set; }
    /// <summary>
    /// The new KDF result that should be used from now on.
    /// </summary>
    public string? KdfResultNewPassword { get; set; }
}

