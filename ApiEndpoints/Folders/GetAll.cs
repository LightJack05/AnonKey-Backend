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
        throw new NotImplementedException();
        
    }
}
