namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Delete
{
    /// <summary>
    /// API endpoint method that deletes an existing credential object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete(string credentialUuid)
    {
          throw new NotImplementedException();      
    }
}
