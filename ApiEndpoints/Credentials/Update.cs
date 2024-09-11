namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Update
{
    
    /// <summary>
    /// API endpoint method that updates a credential object.
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
