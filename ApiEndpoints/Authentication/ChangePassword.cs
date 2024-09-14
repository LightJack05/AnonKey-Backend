namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class ChangePassword
{
    /// <summary>
    /// Changes a users password.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutChangePassword(ApiDatastructures.Authentication.ChangePassword.AuthenticationChangePasswordRequestBody requestBody, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}
