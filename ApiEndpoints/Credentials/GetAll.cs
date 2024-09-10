namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class GetAll
{
    
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.GetAll.CredentialsGetAllResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetAllGet()
    {
        throw new NotImplementedException();    
    }
}
