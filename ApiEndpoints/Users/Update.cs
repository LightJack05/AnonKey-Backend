using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Users;

/// <summary>
/// Handles the users update endpoint.
/// </summary>
public static class Update
{
    /// <summary>
    /// Updates an existing user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutUpdate(ApiDatastructures.Users.Update.UsersUpdateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        User userFromDb = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);

        if (userFromDb is null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "No user found for the associated JWT token.",
                Detail = "There was no user found for this account. Has it been deleted?",
                InternalCode = 0x1
            });
        }

        if (requestBody is null ||
            requestBody.User is null ||
            String.IsNullOrEmpty(requestBody.User.UserName) ||
            String.IsNullOrEmpty(requestBody.User.DisplayName))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A value in the request was null or an empty string.",
                Detail = "Please make sure all values in the request are valid.",
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
        userInfo.DisplayName = requestBody.User.DisplayName;
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }
}
