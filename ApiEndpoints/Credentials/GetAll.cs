namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class GetAll
{
    
    /// <summary>
    /// API endpoint method that gets all available credential objects for this user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.GetAll.CredentialsGetAllResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGetAll()
    {
        throw new NotImplementedException();    
    }
}
