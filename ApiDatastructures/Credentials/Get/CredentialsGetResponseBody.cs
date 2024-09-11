namespace AnonKey_Backend.ApiDatastructures.Credentials.Get;

/// <summary>
/// The body of a response to a credential get request.
/// </summary>
public class CredentialsGetResponseBody
{
    /// <summary>
    /// The credential requested.
    /// </summary>
    public CredentialsGetCredential? Credential { get; set; }
}
