namespace AnonKey_Backend.ApiEndpoints.Authentication;

public static class Login
{

    /// <summary>
    /// Authenticates the user and returns an access token. 
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Authentication.Login.AuthenticationLoginResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostLogin(ApiDatastructures.Authentication.Login.AuthenticationLoginRequestBody requestBody, AnonKey_Backend.Authentication.TokenService tokenService)
    {
        //NO-PROD: This is BS and needs to be replaced with proper authentication!
        Models.UserSecret? user = Models.UserSecret.userSecrets.Where(u =>
        {
            return
                u.Username == requestBody.UserName && u.PasswordHash == requestBody.KdfPasswordResult;
        }).FirstOrDefault();

        // If no user is found matching the redentials, return 404.
        if (user == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody
            {
                Message = "Invalid username or password",
                Detail = "The username and password combination did not match any known combination. Please ensure the password and username are correct.",
                InternalCode = 0x1
            });
        }

        // Generate a new token and return it to the user.
        var token = tokenService.GenerateNewToken(user);
        return TypedResults.Ok(new ApiDatastructures.Authentication.Login.AuthenticationLoginResponseBody
        {
            Token = token,
            ExpiresInSeconds = AnonKey_Backend.Authentication.TokenService.TokenExpiryGraceInSeconds
        });
    }
}
