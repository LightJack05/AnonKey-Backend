namespace AnonKey_Backend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the credentials creation endpoint.
/// </summary>
public static class Create
{

    /// <summary>
    /// Creates a new credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Create.CredentialsCreateResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
     PostCreate(ApiDatastructures.Credentials.Create.CredentialsCreateRequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
