namespace AnonKey_Backend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the editing of credentials.
/// </summary>
public static class Update
{
    
    /// <summary>
    /// Updates a credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Update.CredentialsUpdateResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutUpdate(ApiDatastructures.Credentials.Update.CredentialsUpdateRequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
