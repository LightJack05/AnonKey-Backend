namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class Create
{

    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.Create.FoldersCreateResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostCreate(ApiDatastructures.Folders.Create.FoldersCreateRequestBody requestBody)
    {
        throw new NotImplementedException();
    }
}
