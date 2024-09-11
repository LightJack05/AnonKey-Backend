namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Create
{

    /// <summary>
    /// API endpoint method that creates a new credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Create.CredentialsCreateResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
     PostCreate(ApiDatastructures.Credentials.Create.CredentialsCreateRequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
