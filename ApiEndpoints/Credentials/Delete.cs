namespace AnonKey_Backend.ApiEndpoints.Credentials;

public static class Delete
{
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete(string credentialUuid)
    {
          throw new NotImplementedException();      
    }
}
