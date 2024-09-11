namespace AnonKey_Backend.ApiDatastructures.Authentication.Login;

/// <summary>
/// The body of a login request.
/// </summary>
public class AuthenticationLoginRequestBody
{
    /// <summary>
    /// The name of the user to log in.
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// The result of the KDF function of the user's password.
    /// </summary>
    public string KdfPasswordResult { get; set; }
}

