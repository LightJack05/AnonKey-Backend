namespace AnonKey_Backend.ApiEndpoints.Folders;

/// <summary>
/// Handles the folders create endpoint.
/// </summary>
public static class Create
{

    /// <summary>
    /// Creates a new folder.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.Create.FoldersCreateResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostCreate(ApiDatastructures.Folders.Create.FoldersCreateRequestBody requestBody, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }
}
