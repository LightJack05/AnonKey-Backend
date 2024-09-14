namespace AnonKey_Backend.ApiEndpoints.Users;

/// <summary>
/// Handles the users delete endpoint.
/// </summary>
public static class Delete
{
    /// <summary>
    /// Deletes an existing user.
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
