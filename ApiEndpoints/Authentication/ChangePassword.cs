namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class ChangePassword
{
    /// <summary>
    /// API endpoint method that updates the users password, based on an old and a new kdf result.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutChangePassword(ApiDatastructures.Authentication.ChangePassword.AuthenticationChangePasswordRequestBody requestBody)
    {
        throw new NotImplementedException();
    }
}
