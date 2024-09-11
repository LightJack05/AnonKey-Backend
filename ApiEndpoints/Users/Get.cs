namespace AnonKey_Backend.ApiEndpoints.Users;

public static class Get
{
    /// <summary>
    /// API endpoint method that gets information for an existing user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Users.Get.UsersGetResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet()
    {
        throw new NotImplementedException();
    }
}
