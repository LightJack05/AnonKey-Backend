using AnonKey_Backend.Data;
using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Authentication;

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
                Detail = "The user identity is null. Did you provide a valid JWT token?",
                InternalCode = 0x4
            });
        }
        if (String.IsNullOrEmpty(requestBody.KdfResultNewPassword) || String.IsNullOrEmpty(requestBody.KdfResultOldPassword))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "One of the KDF result values was invalid.",
                Detail = "One of the KDF results in the request was null or an empty string.",
                InternalCode = 0x4
            });
        }

        User? userFromDatabase = databaseHandle.Users.Where(u => u.Username == user.Identity.Name).FirstOrDefault();

        // If no user is found matching the redentials, return 404.
        if (userFromDatabase == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "User not found.",
                Detail = "The lookup for the username in the request did not yeild any results. Please make sure there is a user with the specified name.",
                InternalCode = 0x1
            });
        }

        if (!Cryptography.PasswordHashing.isPasswordValid(requestBody.KdfResultOldPassword, userFromDatabase))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody
            {
                Message = "Invalid password",
                Detail = "The password did not match the password used before. Please ensure the password is correct.",
                InternalCode = 0x1
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
