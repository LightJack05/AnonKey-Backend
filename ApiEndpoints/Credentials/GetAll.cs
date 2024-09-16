namespace AnonKey_Backend.ApiEndpoints.Credentials;

/// <summary>
/// Handles the credentials get all endpoint.
/// </summary>
public static class GetAll
{
    
    /// <summary>
    /// Gets all available credential objects for this user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Credentials.GetAll.CredentialsGetAllResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGetAll(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        throw new NotImplementedException();    
    }
}
