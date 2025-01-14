using System.Text.RegularExpressions;
using AnonKeyBackend.ApiDatastructures.Users.Create;
using AnonKeyBackend.Authentication;
using AnonKeyBackend.Data;

namespace AnonKeyBackend.ApiEndpoints.Users;

/// <summary>
/// Handles the users create endpoint.
/// </summary>
public static class Create
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Users.Create.UsersCreateResponseBody>,
        Conflict<ApiDatastructures.RequestError.ErrorResponseBody>,
        BadRequest<ApiDatastructures.RequestError.ErrorResponseBody>>
           PostCreate(ApiDatastructures.Users.Create.UsersCreateRequestBody requestBody, AnonKeyBackend.Authentication.TokenService tokenService, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        // if (requestBody.UserDisplayName is null || requestBody.KdfPasswordResult is null || requestBody.UserName is null)
        if (String.IsNullOrEmpty(requestBody.UserDisplayName) ||
                String.IsNullOrEmpty(requestBody.UserName) ||
                String.IsNullOrEmpty(requestBody.KdfPasswordResult))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "The a parameter in the request was null",
                Detail = "One of the parameters in the request was null. This is not allowed, please fill in all parameters."
            });
        }

        if (databaseHandle.Users.Any(u => u.Username == requestBody.UserName))
        {
            return TypedResults.Conflict(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "A user with this username already exists.",
                Detail = "There is already a user object for the given username in the database. Please try changing the name and resending the request."
            });
        }

        if (!isUsernameValidWithRestrictions(requestBody.UserName))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "The username supplied does not comply with the restrictions.",
                Detail = """
                                The username is not a valid username. Ensure these rules are followed:
                                - The username is between 5 and 128 characters long.
                                - The username only consists of lowercase and uppercase letters, numbers and underscores (_).
                    """
            });
        }

        Models.User user = CreateNewUser(requestBody, databaseHandle);
        // Generate a new token and return it to the user.
        Models.Token refreshToken = tokenService.GenerateNewToken(user, "RefreshToken");
        Models.Token accessToken = tokenService.GenerateNewToken(user, "AccessToken", refreshToken.Uuid);
        AnonKeyBackend.Authentication.TokenActions.StoreRefreshTokenInDb(refreshToken, databaseHandle);
        databaseHandle.SaveChanges();
        return TypedResults.Ok(new ApiDatastructures.Users.Create.UsersCreateResponseBody
        {
            AccessToken = new()
            {
                Token = accessToken.TokenString,
                TokenType = accessToken.TokenType,
                ExpiryTimestamp = accessToken.ExpiresOn
            },
            RefreshToken = new()
            {
                Token = refreshToken.TokenString,
                TokenType = refreshToken.TokenType,
                ExpiryTimestamp = refreshToken.ExpiresOn
            }
        });


    }

    private static Models.User CreateNewUser(UsersCreateRequestBody requestBody, DatabaseHandle databaseHandle)
    {
        ArgumentNullException.ThrowIfNull(requestBody.KdfPasswordResult);

        string passwordSalt = Cryptography.Generators.NewRandomString(Configuration.Settings.UserPasswordSaltLength);

        Models.User user = new()
        {
            Uuid = Guid.NewGuid().ToString(),
            Username = requestBody.UserName,
            PasswordSalt = passwordSalt,
            PasswordHash = Cryptography.PasswordHashing.HashPassword(requestBody.KdfPasswordResult, passwordSalt)
        };

        databaseHandle.Users.Add(user);
        databaseHandle.UserInfos.Add(new()
        {
            Uuid = Guid.NewGuid().ToString(),
            UserUuid = user.Uuid,
            DisplayName = requestBody.UserDisplayName
        });
        databaseHandle.SaveChanges();

        return user;
    }

    static bool isUsernameValidWithRestrictions(string username)
    {
        return (username.Length >= 5 &&
                username.Length <= 128 &&
                Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"));

    }
}
