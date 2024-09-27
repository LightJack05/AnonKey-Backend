using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Folders;

/// <summary>
/// Handles the folders getall endpoint.
/// </summary>
public static class GetAll
{
    /// <summary>
    /// Gets all folders for a user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.GetAll.FoldersGetAllResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGetAll(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        User userObject = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);
        if (userObject == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user does not exist",
                Detail = "The user does not exist in the database.",
                InternalCode = 0x6
            });
        }
        
    }
}
