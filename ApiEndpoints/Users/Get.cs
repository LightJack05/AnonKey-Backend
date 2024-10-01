using AnonKey_Backend.Models;

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
        User? userFromDb = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);
        if (userFromDb is null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "No user found for the associated JWT token.",
                Detail = "There was no user found for this account. Has it been deleted?",
                InternalCode = 0x1
            });
        }

        UserInfo? userInfo = databaseHandle.UserInfos.FirstOrDefault(ui => ui.UserUuid == userFromDb.Uuid);
        if (userInfo is null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "There was no user information found for this user.",
                Detail = "There was no user information found for this account. This should not happen. Is the database inconsistant? Please recreate the current account.",
                InternalCode = 0x1
            });
        }

        return TypedResults.Ok(new ApiDatastructures.Users.Get.UsersGetResponseBody()
        {
            User = new()
            {
                DisplayName = userInfo.DisplayName
            }
        });

    }
}
