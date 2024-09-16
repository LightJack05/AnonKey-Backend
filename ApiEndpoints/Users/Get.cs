namespace AnonKey_Backend.ApiEndpoints.Users;

/// <summary>
/// Handles the users get endpoint.
/// </summary>
public static class Get
{
    /// <summary>
    /// Gets information for an existing user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Users.Get.UsersGetResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        throw new NotImplementedException();
    }
}
