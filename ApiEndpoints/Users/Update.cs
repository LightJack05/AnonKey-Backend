namespace AnonKey_Backend.ApiEndpoints.Users;

/// <summary>
/// Handles the users update endpoint.
/// </summary>
public static class Update
{
    /// <summary>
    /// Updates an existing user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>> 
            PutUpdate(ApiDatastructures.Users.Update.UsersUpdateRequestBody requestBody)
    {
        throw new NotImplementedException();
    }
}
