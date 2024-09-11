namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Delete
{
    /// <summary>
    /// API endpoint method that deletes an existing user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete()
    {
        throw new NotImplementedException();
    }
}
