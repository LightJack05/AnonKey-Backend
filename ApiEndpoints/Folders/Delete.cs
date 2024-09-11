namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class Delete
{

    /// <summary>
    /// API endpoint method that deletes an existing folder.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete(string folderUuid)
    {
        throw new NotImplementedException();
    }
}
