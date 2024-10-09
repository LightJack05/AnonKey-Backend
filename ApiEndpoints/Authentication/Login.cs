using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Authentication;

/// <summary>
/// Handles the authentication login endpoint.
/// </summary>
public static class Login
{

    /// <summary>
    /// Authenticates the user and returns an access token. 
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Authentication.Login.AuthenticationLoginResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostLogin(ApiDatastructures.Authentication.Login.AuthenticationLoginRequestBody requestBody, AnonKey_Backend.Authentication.TokenService tokenService, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();

        User? user = databaseHandle.Users.Where(u => u.Username == requestBody.UserName).FirstOrDefault();

        if (String.IsNullOrEmpty(requestBody.KdfPasswordResult))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The KDF result was invalid.",
                Detail = "The KDF result in the request was null or an empty string."
            });
        }

        // If no user is found matching the redentials, return 404.
        if (user == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody
            {
                Message = "Invalid username or password",
                Detail = "The username and password combination did not match any known combination. Please ensure the password and username are correct."
            });
        }

        if (!Cryptography.PasswordHashing.isPasswordValid(requestBody.KdfPasswordResult, user))
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody
            {
                Message = "Invalid username or password",
                Detail = "The username and password combination did not match any known combination. Please ensure the password and username are correct."
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
