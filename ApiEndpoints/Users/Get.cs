namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Get
{
    /// <summary>
    /// Gets information for an existing user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Users.Get.UsersGetResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet(ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}
