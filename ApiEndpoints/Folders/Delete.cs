namespace AnonKey_Backend.ApiEndpoints.Folders;

/// <summary>
/// Handles the folders delete endpoint.
/// </summary>
public static class Delete
{

    /// <summary>
    /// Deletes an existing folder.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete(string folderUuid, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}
