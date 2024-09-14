namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Delete
{
    /// <summary>
    /// Deletes an existing credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete(string credentialUuid, ClaimsPrincipal user)
    {
          throw new NotImplementedException();      
    }
}
