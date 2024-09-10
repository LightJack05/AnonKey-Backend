namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Create
{

    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.Create.CredentialsCreateResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
     PostCreate(ApiDatastructures.Credentials.Create.CredentialsCreateRequestBody requestBody)
    {
        throw new NotImplementedException(); 
    }
}
