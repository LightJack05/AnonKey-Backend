using AnonKeyBackend.Data;
using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Authentication;

/// <summary>
/// Changes a users password.
/// </summary>
public static class ChangePassword
{
    /// <summary>
    /// Changes a users password.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutChangePassword(ApiDatastructures.Authentication.ChangePassword.AuthenticationChangePasswordRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        if (user.Identity == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user identity is null",
                Detail = "The user identity is null. Did you provide a valid JWT token?"
            });
        }
        if (String.IsNullOrEmpty(requestBody.KdfResultNewPassword) || String.IsNullOrEmpty(requestBody.KdfResultOldPassword))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "One of the KDF result values was invalid.",
                Detail = "One of the KDF results in the request was null or an empty string."
            });
        }

        User? userFromDatabase = databaseHandle.Users.Where(u => u.Username == user.Identity.Name).FirstOrDefault();

        // If no user is found matching the redentials, return 404.
        if (userFromDatabase == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "User not found.",
                Detail = "The lookup for the username in the request did not yeild any results. Please make sure there is a user with the specified name."
            });
        }

        if (!Cryptography.PasswordHashing.isPasswordValid(requestBody.KdfResultOldPassword, userFromDatabase))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody
            {
                Message = "Invalid password",
                Detail = "The password did not match the password used before. Please ensure the password is correct."
            });
        }

        ChangeUserPassword(userFromDatabase, requestBody.KdfResultNewPassword, databaseHandle);

        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }

    private static void ChangeUserPassword(User user, string newPassword, DatabaseHandle databaseHandle)
    {
        string passwordSalt = Cryptography.Generators.NewRandomString(Configuration.Settings.UserPasswordSaltLength);

        user.PasswordSalt = passwordSalt;
        user.PasswordHash = Cryptography.PasswordHashing.HashPassword(newPassword, passwordSalt);
    }
}
