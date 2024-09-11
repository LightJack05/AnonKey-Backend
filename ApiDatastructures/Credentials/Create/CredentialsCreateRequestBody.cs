namespace AnonKey_Backend.ApiDatastructures.Credentials.Create;

/// <summary>
/// The body of a credential create request.
/// </summary>
public class CredentialsCreateRequestBody
{
    /// <summary>
    /// The credential to be created.
    /// </summary>
    public CredentialsCreateCredential? Credential { get; set; }
}
