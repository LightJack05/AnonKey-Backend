namespace AnonKeyBackend.ApiDatastructures.Credentials.GetAll;

/// <summary>
/// Response to a get all credentials request.
/// </summary>
public class CredentialsGetAllResponseBody
{
    /// <summary>
    /// A list of credentials associated with the user.
    /// </summary>
    public List<CredentialsGetAllCredential>? Credentials { get; set; }
}
